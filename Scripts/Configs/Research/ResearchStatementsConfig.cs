using System.Linq;
using Libraries.Sparky.Configs.Fire;

namespace Libraries.Sparky.Configs.Research
{
    public class ResearchStatementsConfig : IStaticConfig
    {
        // Fields
        // Public
        public int TheoryId;
        public ResearchStatementConfig[] Statements;

        public bool IsValid() =>
            Statements.Length == Statements.Distinct().Count();

        public string Title { get; set; }
        public string Description { get; set; }
        public string GetDescription() => 
            throw new System.NotImplementedException();

        public void Run() => 
            throw new System.NotImplementedException();
    }
}