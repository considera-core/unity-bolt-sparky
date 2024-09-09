using Libraries.Bolt;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Card;
using Libraries.Bolt.Objects.Components.Table;
using Libraries.Game.Helpers;
using Libraries.Sparky.Configs.Water;

namespace Libraries.Sparky.Objects.Components.Cards.Water.Sources
{
    public class WaterSourcesTableView : BaseExtension
    {
        // Fields
        // Private
        // Private Serialized
        public Card _card;
        private Table _table;
        
        // Properties
        // Private
        private Card Card => _card ??= GetComponent<Card>();
        private Table Table => _table ??= GetComponentInChildren<Table>(true);
        
        // Methods
        // Base
        public override IObject Build()
        {
            Card.Build(title: "Water Sources");
            Table.Build(WaterConfigs.WaterSourcesTableConfig);
            
            return this;
        }
        
        public override void OnStart()
        {
            StartTable();
        }

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
            WaterConfigs.WaterSources.ForEach(x =>
            {
                _table.UpdateBody(0, row, x.Title);
                row++;
            });
        }
        
        private void UpdateTable()
        {
            var row = 0;
            WaterConfigs.WaterSources.ForEach(x =>
            {
                _table.UpdateBody(1, row, $"+{x.ExtractionConfig.ExtractWater:N2}L <color=green>({Strings.Percent(x.ExtractionConfig.ExtractWaterChance, 100)})");
                _table.UpdateBody(2, row, $"+{x.ExtractionConfig.ExtractWaterPolluted:N2}L <color=red>({Strings.Percent(x.ExtractionConfig.ExtractWaterPollutedChance, 100)})");
                _table.UpdateBody(3, row, $"+{x.ExtractionConfig.ExtractXP:N2}XP");
                _table.UpdateBody(4, row, x.ExtractionConfig.GetExtractProgressPercent());
                _table.UpdateBody(5, row, "<actions>");
                row++;
            });
        }
    }
}