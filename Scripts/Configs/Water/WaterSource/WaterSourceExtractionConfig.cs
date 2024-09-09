// ReSharper disable MemberCanBePrivate.Global

using System;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data;
using Libraries.Game.Models.Data.Skills;
using UnityEngine;
using static Libraries.Game.Helpers.SpriteStrings;
using static Libraries.Game.Helpers.Strings;
using Random = System.Random;

namespace Libraries.Sparky.Configs.Water.WaterSource
{
    public class WaterSourceExtractionConfig
    {        
        private WaterData WaterData => DataController.WaterData;
        private Skill WaterSkill => DataController.WaterSkill.Water;

        public int Id { get; set; }
        public double Capacity { get; set; }
        public bool Extracting { get; set; }
        public double ExtractWater { get; set; }
        public double ExtractWaterPolluted { get; set; }
        public float ExtractWaterChance { get; set; }
        public float ExtractWaterPollutedChance { get; set; }
        public double ExtractXP { get; set; }
        public float ExtractTimeLimit { get; set; }
        public float ExtractTime { get; set; }

        private static readonly Random Rng = new();

        public void Run()
        {
            if (!Extracting) return;
            if (!CanExtract()) return;

            UpdateExtractTime();
            
            if (!IsExtractReady()) return;

            Extract();
        }

        public string GetDescription() =>
            $"+{ExtractWater:N2}L{ResourceWater}: {PercentChance(ExtractWaterChance)}\n" +
            $"+{ExtractWaterPolluted:N2}L{ResourceWaterPolluted}: {PercentChance(ExtractWaterPollutedChance)}\n" +
            $"+{ExtractXP:N2}{FeatureWater} XP";

        public string GetTankLabel(float tankPercent) =>
            $"{WaterData.WaterSources[0]:N2}L\n<size=8>{Capacity:N2}L\n<size=12>{tankPercent:F0}%";
        
        public string GetExtractLabel() =>
            $"{(Extracting ? "Extracting" : "Paused")}... {ExtractTimeLimit - ExtractTime:F1}s";
        
        public string GetExtractProgressPercent() =>
            $"{ExtractTime / ExtractTimeLimit * 100:F0}%";

        private bool CanExtract() => 
            WaterData.WaterSources[0] > 0 && TimeController.DeltaTime > 0 && Time.timeScale > 0;

        private void UpdateExtractTime() => 
            ExtractTime += TimeController.DeltaTime * (1 / Time.timeScale);

        private bool IsExtractReady() => 
            ExtractTime >= ExtractTimeLimit;

        private void Extract()
        {
            var isWater = Rng.NextDouble() <= ExtractWaterChance;
            var amount = Math.Min(
                isWater ? ExtractWater : ExtractWaterPolluted, 
                WaterData.WaterSources[0]);

            if (isWater) WaterData.Water += amount;
            else WaterData.WaterPolluted += amount;
            
            WaterSkill.AddXp(ExtractXP);
            WaterData.WaterSources[0] -= amount;
            
            ResetExtract();
        }

        private void ResetExtract() => 
            ExtractTime = 0;
    }
}
