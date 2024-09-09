using Libraries.Bolt;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects;
using Libraries.Game.Enums.Types;
using Libraries.Sparky.Objects.Components.Pages;
using UnityEngine;

namespace Libraries.Sparky
{
    [DisallowMultipleComponent]
    public class SparkyPages : BaseExtension
    {
        // Fields
        // Private
        private WaterPage _waterPage;
        private MiningPage _miningPage;
        private SkillingPage _skillingPage;
        private GatheringPage _gatheringPage;
        private SurvivalPage _survivalPage;
        private ResearchPage _researchPage;
        
        // Properties
        private WaterPage WaterPage => GetComponentInChildren(ref _waterPage);
        private MiningPage MiningPage => GetComponentInChildren(ref _miningPage);
        private SkillingPage SkillingPage => GetComponentInChildren(ref _skillingPage);
        private GatheringPage GatheringPage => GetComponentInChildren(ref _gatheringPage);
        private SurvivalPage SurvivalPage => GetComponentInChildren(ref _survivalPage);
        private ResearchPage ResearchPage => GetComponentInChildren(ref _researchPage);

        // Methods
        // Base Methods
        public override IObject Build()
        {
            WaterPage.Build();
            MiningPage.Build();
            SkillingPage.Build();
            GatheringPage.Build();
            SurvivalPage.Build();
            ResearchPage.Build();

            return this;
        }
        
        public override void OnStart()
        {
            WaterPage.OnStart();
            MiningPage.OnStart();
            SkillingPage.OnStart();
            GatheringPage.OnStart();
            SurvivalPage.OnStart();
            ResearchPage.OnStart();
        }
        
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            WaterPage.OnUpdate();
            MiningPage.OnUpdate();
            SkillingPage.OnUpdate();
            GatheringPage.OnUpdate();
            SurvivalPage.OnUpdate();
            ResearchPage.OnUpdate();

            return true;
        }
        
        public override void OnRun()
        {
            WaterPage.OnRun();
            MiningPage.OnRun();
            SkillingPage.OnRun();
            GatheringPage.OnRun();
            SurvivalPage.OnRun();
            ResearchPage.OnRun();
        }

        // Public Methods
        public void SetPageActive(PageType page)
        {
            SetAllActive(false);
            
            switch (page)
            {
                case PageType.Water: SetWaterActive(true); break;
                case PageType.Mining: SetMiningActive(true); break;
                case PageType.Skilling: SetSkillingActive(true); break;
                case PageType.Gathering: SetGatheringActive(true); break;
                case PageType.Survival: SetSurvivalActive(true); break;
                case PageType.Research: SetResearchActive(true); break;
                default: case PageType.Undefined: break;
            }
        }

        // Private Methods
        private void SetAllActive(bool active)
        {
            SetWaterActive(active);
            SetMiningActive(active);
            SetSkillingActive(active);
            SetGatheringActive(active);
            SetSurvivalActive(active);
            SetResearchActive(active);
        }

        private void SetWaterActive(bool active) =>
            WaterPage.SetActive(active);

        private void SetMiningActive(bool active) =>
            MiningPage.SetActive(active);
        
        private void SetSkillingActive(bool active) =>
            SkillingPage.SetActive(active);
        
        private void SetGatheringActive(bool active) =>
            GatheringPage.SetActive(active);
        
        private void SetSurvivalActive(bool active) =>
            SurvivalPage.SetActive(active);
        
        private void SetResearchActive(bool active) =>
            ResearchPage.SetActive(active);
    }
}