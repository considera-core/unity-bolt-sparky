// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Libraries.Game.Controllers;
using Libraries.Game.Models.Data;
using Libraries.Game.Models.Data.Skills;
using UnityEngine;

namespace Libraries.Sparky.Configs.Fire
{
    public class FiremakingConfig : IConfig
    {
        public static readonly FiremakingConfig Firemaking = new()
        {
            Title = "Fire 101",
            Description = "I don't want to be cold. It's bothering me and I want it to stop. So I'll just light a little fire!",
            HydrationCost = 0.05f,
            TwigCost = 5.0f, // todo: wood cost; wood type (twig, log, etc)
            FireAmount = 1.0f,
            BaseIgniteTime = 30.0f,
            BaseFireTime = 360.0f,
            SurvivalXp = 1.0f,
            FireXp = 1.0f,
            YouEduranceXp = 1.0f,
            YouComfortXpPS = 0.1f
        };
        
        // Properties
        // Private
        public SurvivalData Survival => DataController.SurvivalData;
        public Skill ComfortSkill => DataController.YouComfortSkill;
        public Skill SurvivalSkill => DataController.SurvivalSkill;
        public Skill FireSkill => DataController.FireSkill;
        public Skill EnduranceSkill => DataController.YouEnduranceSkill;
        // Public
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Igniting { get; set; }
        public bool Ignited { get; set; }
        public float HydrationCost { get; set; }
        public float TwigCost { get; set; }
        public float FireAmount { get; set; }
        public float BaseIgniteTime { get; set; }
        public float BaseFireTime { get; set; }
        public float SurvivalXp { get; set; }
        public float FireXp { get; set; }
        public float YouEduranceXp { get; set; }
        public float YouComfortXpPS { get; set; } // todo: FloatPS struct?
        
        // todo random thought (): what if for every feature, you gain intelligence xp but the reward dwindles fast
        // as you get better at it, so you have to keep learning new things to keep gaining intelligence xp

        // todo: there will be different types of fire scores, the higher they are,
        // the less comfort you will get from them, but you get more xp and other
        // benefits and fire in return
        
        // todo: i can totally make each card and part of the UI customizable. presets are free but full custom is IAP

        // Methods
        // Base Methods
        public void Run()
        {
            RunIgnitedState();
            RunIgnitingState();
        }

        // Private
        private void RunIgnitedState()
        {
            if (!Ignited) return;
            
            ComfortSkill.AddXp(YouComfortXpPS.PS());
            Survival.FireTime += TimeController.DeltaTime;
            if (Survival.FireTime < BaseFireTime) return;
            
            Survival.FireTime = 0;
            Ignited = false;
            Survival.Fire -= FireAmount; // maybe i can just do a Sum(x => x.FireAmount) instead of storing fire. this would be our requirement currency for anything with fire? Or Fire always goes up...? Maybe Active Fire acts more as slots for fire working related features
        }

        private void RunIgnitingState()
        {
            if (!Igniting) return;
            
            Survival.IgniteTime += TimeController.DeltaTime;
            if (Survival.IgniteTime < BaseIgniteTime) return;
            
            SurvivalSkill.AddXp(SurvivalXp);
            FireSkill.AddXp(FireXp);
            EnduranceSkill.AddXp(YouEduranceXp);
            Survival.FireTime = 0;
            Survival.IgniteTime = 0;
            Igniting = false;
            Ignited = true;
            Survival.Fire += FireAmount;
        }

        // Public
        public void StartIgnite()
        {
            if (!CanIgnite()) return;
            
            Igniting = true;

            DataController.CoreData.You.Hydration -= HydrationCost;
            DataController.CoreData.Gathering.Twigs -= TwigCost;
        }
        
        public float GetIgnitePercentage() => 
            Survival.IgniteTime / BaseIgniteTime;
        
        public float GetFirePercentage() => 
            Survival.FireTime / BaseFireTime;
        
        public float GetIgniteTimeRemaining() => 
            BaseIgniteTime - Survival.IgniteTime;
        
        public float GetFireTimeRemaining() =>
            BaseFireTime - Survival.FireTime;
        
        public string GetDescription() =>
            $"{Description} Requires {TwigCost} Twigs. Dehydrates You -{HydrationCost}L\n\n" +
            "Ignited Benefits:\n" +
            "[+] 10% Game Speed\n" +
            $"[+] {FireAmount:N0} Fire";

        public bool CanIgnite() => 
            DataController.CoreData.You.Hydration >= HydrationCost 
            && DataController.CoreData.Gathering.Twigs >= TwigCost
            && !Igniting
            && !Ignited
            && TimeController.DeltaTime > 0 
            && Time.timeScale > 0;
    }

    public interface IStaticConfig : IConfig
    {
    }

    public interface IDynamicConfig : IConfig
    {
        public string Title { get; set; }
        public string Description { get; set; }
        string GetDescription();
        void Run();
    }

    public interface IConfig
    {
        
    }
}