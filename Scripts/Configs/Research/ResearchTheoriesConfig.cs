using System.Collections.Generic;
using Libraries.Bolt.Configs.Components.Table;

namespace Libraries.Sparky.Configs.Research
{
    public class ResearchTheoriesConfig
    {
        public string Name;
        public int TheoryId;
        public ResearchStatementsConfig StatementsConfig;
        public ResearchTheoremsConfig TheoremsConfig;
        public ResearchDescriptionConfig DescriptionConfig;
        public ResearchProvingConfig ProvingConfig;
        
        // Methods
        // Base
        public void Run()
        {
            
        }
        
        public static TableConfig ResearchStatementTableConfig(ResearchStatementsConfig config) =>
            new(ResearchStatementTableColumns, config.Statements.Length);
        
        public static TableConfig ResearchTheoremTableConfig(ResearchTheoremsConfig config) =>
            new(ResearchTheoremTableColumns, config.Theorems.Length);

        private static readonly List<TableColumnConfig> ResearchStatementTableColumns = new()
        {
            new TableColumnConfig("Statement", 0.20f),
            new TableColumnConfig("Description", 0.50f),
            new TableColumnConfig("Proven", 0.15f),
            new TableColumnConfig("Action", 0.15f)
        };
        
        private static readonly List<TableColumnConfig> ResearchTheoremTableColumns = new()
        {
            new TableColumnConfig("Logic", 0.30f),
            new TableColumnConfig("Translation", 0.55f),
            new TableColumnConfig("Proven", 0.15f)
        };
    }
}