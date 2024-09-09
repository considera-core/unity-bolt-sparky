using System.Collections.Generic;
using Libraries.Bolt.Configs.Components.Table;
using Libraries.Game.Enums;
using Libraries.Game.Enums.Types;
using Libraries.Sparky.Configs.Water.WaterSource;

namespace Libraries.Sparky.Configs.Water
{
    public static class WaterConfigs
    {
        private static readonly WaterSourcePanningConfig[] WaterSourcePannings =
        {
            new()
            {
                Id = 0,
                DustType = MiningResource.Stone,
                ToolTypeType = PanningToolType.BareHands,
                HydrationCost = 0.01,
                DustAmount = 0.1,
                MiningXPReward = 1,
                PanningXPReward = 1,
                WaterXPReward = 1,
                StrengthXPReward = 1,
                PanningTimeLimit = 60,
            },
            new()
            {
                Id = 1,
                DustType = MiningResource.Stone,
                ToolTypeType = PanningToolType.BareHands,
                HydrationCost = 0.01,
                DustAmount = 0.1,
                MiningXPReward = 1,
                PanningXPReward = 1,
                WaterXPReward = 1,
                StrengthXPReward = 1,
                PanningTimeLimit = 60,
            },
        };

        private static readonly WaterSourceExtractionConfig[] WaterSourceExtractions = 
        {
            new()
            {
                Id = 0,
                Capacity = 5,
                ExtractWater = 0.01,
                ExtractWaterChance = 0.1f,
                ExtractWaterPolluted = 0.01,
                ExtractWaterPollutedChance = 0.9f,
                ExtractXP = 1,
                ExtractTimeLimit = 2,
            },
            new()
            {
                Id = 1,
                Capacity = 5,
                ExtractWater = 0.01,
                ExtractWaterChance = 0.1f,
                ExtractWaterPolluted = 0.01,
                ExtractWaterPollutedChance = 0.9f,
                ExtractXP = 1,
                ExtractTimeLimit = 2,
            },
        };

        public static readonly List<WaterSourceConfig> WaterSources = new()
        {
            new WaterSourceConfig
            {
                Id = 0,
                Title = "Dirty Puddle",
                ShortTitle = "Puddle",
                ExtractionConfig = WaterSourceExtractions[0],
                PanningConfig = WaterSourcePannings[0],
            },
            new WaterSourceConfig
            {
                Id = 1,
                Title = "Stream",
                ShortTitle = "Stream",
                ExtractionConfig = WaterSourceExtractions[0],
                PanningConfig = WaterSourcePannings[0],
            }
        };
        
        private static readonly List<TableColumnConfig> WaterSourcesTableColumns = new()
        {
            new TableColumnConfig("Sources", 0.15f),
            new TableColumnConfig("Water", 0.15f),
            new TableColumnConfig("Polluted", 0.15f),
            new TableColumnConfig("XP", 0.15f),
            new TableColumnConfig("Progress", 0.15f),
            new TableColumnConfig("Actions", 0.25f),
        };

        public static readonly TableConfig WaterSourcesTableConfig =
            new(WaterSourcesTableColumns, WaterSources.Count);
    }
}