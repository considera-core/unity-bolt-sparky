// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using Libraries.Game.Controllers;
using Libraries.Sparky.Enums.Gathering;
using UnityEngine;

namespace Libraries.Sparky.Configs.Gathering
{
    public class GatheringPlaceConfig
    {
        public static readonly GatheringPlaceConfig GatheringTwigsPlace = new()
        {
            Title = "Outdoors: Twigs",
            Description = "Go outside and find some wood. Who up finding thy sticks?",
            Type = GatheringPlaceType.Twigs,
            HydrationCost = 0.01f,
            GatherAmount = 1.0f,
            GatherTimeLimit = 60,
            GatherXpAmount = 1.0f,
            YouEduranceXpAmount = 1.0f,
        };
        
        public static readonly GatheringPlaceConfig GatheringRocksPlace = new()
        {
            Title = "Outdoors: Rocks",
            Description = "Rocks are also good for throwing. And building. And... well, you get the idea.",
            Type = GatheringPlaceType.Rocks,
            HydrationCost = 0.01f,
            GatherAmount = 1.0f,
            GatherTimeLimit = 120,
            GatherXpAmount = 3.0f,
            YouEduranceXpAmount = 3.0f,
        };
        
        public static readonly GatheringPlaceConfig GatheringSandPlace = new()
        {
            Title = "Outdoors: Sand",
            Description = "Sand is good for... well, it's good for something. Oh, Sand Castles!!",
            Type = GatheringPlaceType.Sand,
            HydrationCost = 0.02f,
            GatherAmount = 2.0f,
            GatherTimeLimit = 30,
            GatherXpAmount = 1.0f,
            YouEduranceXpAmount = 1.0f,
        };
        
        public string Title { get; set; }
        public string Description { get; set; }
        public GatheringPlaceType Type { get; set; }
        public bool Gathering { get; set; }
        public float HydrationCost { get; set; }
        public float GatherAmount { get; set; }
        public float GatherTimeLimit { get; set; }
        public float GatherTime { get; set; }
        public float GatherXpAmount { get; set; }
        public float YouEduranceXpAmount { get; set; }

        public void Run()
        {
            if (!Gathering) return;

            if (!CanGather()) return;

            UpdateGatherTime();

            if (!IsGatherTime()) return;

            Gather();
        }

        public string GetDescription() =>
            $"{Description} Dehydrates {HydrationCost:F2}L.\n\n" +
            "Gather Amount:\n" +
            $"{GatherAmount:N2} {Type.ToString()}";

        public float GetTimeRemaining() => 
            GatherTimeLimit - GatherTime;

        public bool CanGather() => 
            DataController.CoreData.You.Hydration >= HydrationCost 
            && TimeController.DeltaTime > 0 
            && Time.timeScale > 0;

        private void UpdateGatherTime() => 
            GatherTime += TimeController.DeltaTime;

        private bool IsGatherTime() => 
            GatherTime >= GatherTimeLimit;

        private void Gather()
        {
            DataController.GatheringData.Twigs += GatherAmount;
            DataController.CoreData.You.Hydration -= HydrationCost;
            
            DataController.GatheringSkill.AddXp(GatherXpAmount);
            DataController.YouEnduranceSkill.AddXp(YouEduranceXpAmount);

            ResetGather();
        }

        private void ResetGather()
        {
            Gathering = false;
            GatherTime = 0;
        }
    }
}