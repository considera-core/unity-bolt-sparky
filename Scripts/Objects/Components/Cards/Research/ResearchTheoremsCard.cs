using System.Linq;
using Libraries.Bolt.Extensions.UI.Cards;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Table;
using Libraries.Sparky.Configs.Research;

namespace Libraries.Sparky.Objects.Components.Cards.Research
{
    public class ResearchTheoremsCard : CardExtension
    {
        // Fields
        // Private
        private Table _table;
        private ResearchTheoremsConfig _config;
        
        // Properties
        // Private
        private Table Table => GetComponentInChildren(ref _table);
        
        // Methods
        // Base
        public IObject Build(ResearchTheoremsConfig config)
        {
            _config = config;
            Card.Build(title: $"Theorems ({config.Theorems.Length})");
            Table.Build(ResearchTheoriesConfig.ResearchTheoremTableConfig(config));

            return this;
        }
        
        public override void OnStart() => 
            StartTable();

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            UpdateTable();
            
            return true;
        }

        // Private
        private void StartTable()
        {
            var row = 0;
            _config.Theorems.ToList().ForEach(x =>
            {
                Table.UpdateBody(0, row, x.Logic);
                Table.UpdateBody(1, row, x.Translation);
                Table.UpdateBody(2, row, "<color=red>FALSE</color>");
                row++;
            });
        }

        private void UpdateTable()
        {
            var row = 0;
            _config.Theorems.ToList().ForEach(x =>
            {
                Table.UpdateBody(2, row, IsProven(row) ? "<color=green>TRUE</color>" : "<color=red>FALSE</color>");
                row++;
            });
        }
        
        private bool IsProven(int row) => 
            _config.Theorems[row].GetStatements().All(x => x.IsProved());
    }
}