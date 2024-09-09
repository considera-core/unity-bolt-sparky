// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System;
using Libraries.Game.Controllers;
using Libraries.Game.Helpers;
using Libraries.Game.Models.Data.Skills;
using UnityEngine;
using Random = System.Random;

namespace Libraries.Sparky.Configs.Water
{
    public class WaterAnalysisConfig
    {
        public static readonly WaterAnalysisConfig WaterAnalysis = new()
        {
            Title = "Pollution Analysis",
            WaterChance = 0.1f,
            AnalyzeVolume = 0.1,
            Analyzing = false,
            AnalyzeTimeLimit = 60,
            AnalyzeTime = 0,
            AnalyzingSuccessXp = 10.0f,
            AnalyzingFailXp = 1.0f,
        };
        
        // Fields
        // Private
        // Private Static
        private static readonly Random Rng = new();
        
        // Properties
        // Public
        public string Title { get; set; }
        public float WaterChance { get; set; }
        public double AnalyzeVolume { get; set; }
        public bool Analyzing { get; set; }
        public float AnalyzeTimeLimit { get; set; }
        public float AnalyzeTime { get; set; }
        public float AnalyzingSuccessXp { get; set; }
        public float AnalyzingFailXp { get; set; }
        // Private
        public Skill IntelligenceSkill => DataController.CoreData.Skills.You.Intelligence;
        public Skill AnalyzingSkill => DataController.CoreData.Skills.Analyzing;

        // Methods
        // Base Methods
        public void Run()
        {
            if (!Analyzing) return;

            if (!CanAnalyze()) return;

            UpdateAnalyzeTime();

            if (!IsAnalysisTime()) return;

            Analyze();
        }

        // Public Methods
        public string GetDescription() =>
            "Analyze Polluted Water for a chance of collecting Clean Water and some other bonuses.\n\n" +
            "Analyze Volume:\n" +
            $"{AnalyzeVolume:N2}L{SpriteStrings.ResourceWaterPolluted}";

        public string GetVolume() =>
            $"Analyze Volume: {AnalyzeVolume:N2}";

        public float GetTimeRemaining() => 
            AnalyzeTimeLimit - AnalyzeTime;

        public bool CanAnalyze() => 
            DataController.WaterData.WaterPolluted >= AnalyzeVolume 
            && TimeController.DeltaTime > 0 
            && Time.timeScale > 0;

        // Private Methods
        private void UpdateAnalyzeTime() => 
            AnalyzeTime += TimeController.DeltaTime * (1 / Time.timeScale);

        private bool IsAnalysisTime() => 
            AnalyzeTime >= AnalyzeTimeLimit;

        private void Analyze()
        {
            var isWater = Rng.NextDouble() <= WaterChance;
            var amount = Math.Min(AnalyzeVolume, DataController.WaterData.WaterPolluted);

            if (isWater)
            {
                DataController.WaterData.Water += amount;
                AnalyzingSkill.AddXp(AnalyzingSuccessXp);
            }
            else
            {
                DataController.WaterData.AddWaterToSources(amount);
                AnalyzingSkill.AddXp(AnalyzingFailXp);
            }

            DataController.WaterData.WaterPolluted -= amount;
            
            IntelligenceSkill.AddXp(1.0f);
            

            ResetAnalyze();
        }

        private void ResetAnalyze()
        {
            Analyzing = false;
            AnalyzeTime = 0;
        }
    }
}