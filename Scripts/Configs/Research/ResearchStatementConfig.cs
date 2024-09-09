using System;
using System.Collections.Generic;
using System.Linq;
using Libraries.Bolt.Objects.Components.Table;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data.Research;
using Libraries.Sparky.Enums;
using UnityEngine;

namespace Libraries.Sparky.Configs.Research
{
    public class ResearchStatementConfig
    {
        // Constants
        public const float ProofTimeDefault = 10.0f;
        
        // Fields
        // Public
        public int StatementId;
        public int TheoryId;
        public readonly string Statement;
        public readonly string Description;
        // Private
        private event OnProvedCallback OnProved;
        private readonly Dictionary<ResourceType, double> _requirements;
        private readonly string[] _dependencies;
        private float _proofTime;
        private bool _proving;
        private Table ref_table;
        
        // Properties
        // Private
        private ResearchData Research => DataController.CoreData.Research;
        private ResearchTheoryData TheoryData => Research.Theories[TheoryId];
        private List<ResearchTheoryStatementData> Statements => TheoryData.Statements;
        private ResearchTheoryStatementData StatementData => Statements[StatementId];
        
        // Delegates
        // Public
        public delegate void OnProvedCallback();
        
        // Constructors
        public ResearchStatementConfig(
            string statement, 
            string description, 
            string[] dependencies = null,
            Dictionary<ResourceType, double> requirements = null)
        {
            Statement = statement;
            Description = description;
            _dependencies = dependencies ?? Array.Empty<string>();
            _requirements = requirements ?? new Dictionary<ResourceType, double>();
            _proofTime = 0.0f;
        }
        
        // Methods
        // Base
        public void Build(Table table) => 
            ref_table = table;

        public void Run()
        {
            if (!_proving) return;
            
            UpdateProofTime();
            
            if (!IsProofComplete()) return;
            
            CompleteProof();
        }
        
        // Public
        public bool IsProved() => 
            StatementData.Proven;
        
        public float GetProofTime() => 
            ProofTimeDefault;
        
        public float GetTimeRemaining() => 
            ProofTimeDefault - _proofTime;
        
        public float GetProofProgress() =>
            _proofTime / ProofTimeDefault;

        public string GetDependencyStatus()
        {
            if (IsProved()) return "<color=#4CFFFC>PROVED!"; // todo: make static strings for color hex codes
            if (IsProvable()) return "<color=green>Requirements fulfilled!";
            const string result = "<color=red>Can't. ";
            var dependencies = string.Join(", ", Statements
                .Where(x => _dependencies.Contains(x.Statement) && !x.Proven)
                .Select(x => x.Statement)
                .ToList());
            return string.Concat(result, dependencies, $" {(dependencies.Length > 1 ? "are" : "is")} FALSE!");
        }

        public List<ResearchStatementConfig> GetDependencies(ResearchStatementsConfig parentConfig) =>
            parentConfig.Statements
                .Where(x => _dependencies.Contains(x.Statement))
                .ToList();
        
        public void Prove()
        {
            if (!CanProve()) return;
            _proving = true;
        }
        
        public void SubscribeToOnProved(OnProvedCallback callback) => 
            OnProved += callback;

        // Private
        private bool CanProve() => 
            !_proving 
            && !StatementData.Proven 
            && IsProvable();
        
        private bool IsProvable() => 
            _dependencies.All(x => TheoryData.Statements.Any(y => y.Statement == x && y.Proven)) 
            && RequirementsMet();
        
        private bool RequirementsMet() => 
            _requirements.All(x => x.Value <= ResourceTable.GetResourceValue(x.Key));
        
        private bool IsProofComplete() =>
            _proofTime >= ProofTimeDefault;
        
        private void UpdateProofTime() => 
            _proofTime += TimeController.DeltaTime * (1 / Time.timeScale);

        private void CompleteProof()
        {
            _proving = false;
            _proofTime = 0.0f;
            StatementData.Proven = true;
            OnProved?.Invoke();
            //ref_table.UpdateBody(2, StatementId, "<color=green>TRUE</color>");
        }
    }
}