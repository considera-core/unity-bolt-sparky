using System;
using System.Collections.Generic;

namespace Libraries.Sparky.Configs.Research
{
    public class ResearchTheoremConfig
    {
        // Fields
        // Public
        public int TheoryId;
        public int TheoremId;
        public string Logic;
        public string Translation;
        public readonly string[] Statements;
        // Private
        private List<ResearchStatementConfig> _statementConfigs;

        // Constructors
        public ResearchTheoremConfig(
            string logic, 
            string translation,
            string[] statements = null)
        {
            Logic = logic;
            Translation = translation;
            Statements = statements ?? Array.Empty<string>();
        }
        
        // Methods
        // Public
        public List<ResearchStatementConfig> GetStatements() =>
            _statementConfigs;
        
        public void AssignStatements(List<ResearchStatementConfig> statementConfigs) =>
            _statementConfigs = statementConfigs;
    }
}