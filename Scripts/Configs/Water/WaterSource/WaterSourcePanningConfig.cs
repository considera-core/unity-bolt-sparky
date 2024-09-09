// ReSharper disable MemberCanBePrivate.Global

using Libraries.Game.Controllers;
using Libraries.Game.Enums;
using Libraries.Game.Enums.Extensions;
using Libraries.Game.Enums.Types;
using Libraries.Game.Models.Data;
using Libraries.Game.Models.Data.Skills;
using UnityEngine;
using Random = System.Random;

namespace Libraries.Sparky.Configs.Water.WaterSource
{
    public class WaterSourcePanningConfig
    {
        // Properties
        // Public
        public int Id { get; set; }
        public MiningResource DustType { get; set; }
        public PanningToolType ToolTypeType { get; set; }
        public bool Panning { get; set; }
        public double HydrationCost { get; set; }
        public double DustAmount { get; set; }
        public double MiningXPReward { get; set; }
        public double PanningXPReward { get; set; } // panning is a subskill of mining and water, or just mining... could do some complex interactions between main and subskills
        public double WaterXPReward { get; set; }
        public double StrengthXPReward { get; set; }
        public float PanningTimeLimit { get; set; }
        public float PanningTime { get; set; }
        // Private
        private YouData You => DataController.CoreData.You;
        private Skill StrengthSkill => DataController.YouStrengthSkill;
        private Skill WaterSkill => DataController.CoreData.Skills.Water.Water;
        private Skill PanningSkill => DataController.CoreData.Skills.Panning;
        private Skill MiningSkill => DataController.CoreData.Skills.Mining;
        // Private Static
        private static readonly Random Rng = new();

        // Methods
        // Public
        public void Run()
        {
            if (!Panning) return;
            if (!CanPan()) return;

            UpdateTime();
            CompletePanning();
        }

        public string GetDescription() =>
            $"With {ToolTypeType.ToName()}:\n" +
            $"[-] {HydrationCost}L Hydration\n" +
            $"[+] {DustAmount}g {DustType.ToName()} Dust\n" +
            $"[+] {MiningXPReward} Mining XP\n" +
            $"[+] {PanningXPReward} Panning XP\n" +
            $"[+] {WaterXPReward} Water XP";

        public string GetPanningLabel() =>
            $"{(Panning ? "Panning" : "Paused")}... {PanningTimeLimit - PanningTime:F1}s";

        // Private
        private bool CanPan() => 
            TimeController.DeltaTime > 0 && Time.timeScale > 0 && You.Hydration >= HydrationCost;

        private void UpdateTime() => 
            PanningTime += TimeController.DeltaTime * (1 / Time.timeScale);

        private bool IsExtractReady() => 
            PanningTime >= PanningTimeLimit;

        // todo: some generic wizard magic to work on!
        private void CompletePanning()
        {
            if (!Panning || PanningTime < PanningTimeLimit) return;
            
            DataController.MiningData.Dust[DustType.ToId()] += DustAmount;
            
            MiningSkill.AddXp(MiningXPReward);
            PanningSkill.AddXp(PanningXPReward);
            WaterSkill.AddXp(WaterXPReward);
            StrengthSkill.AddXp(StrengthXPReward);
            
            You.Hydration -= HydrationCost;
            
            ResetJob();
        }

        private void ResetJob()
        {
            PanningTime = 0;
            Panning = false;
        }
    }
}
