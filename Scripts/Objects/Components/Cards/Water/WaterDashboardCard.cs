using Libraries.Bolt;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects.Components.Button;
using Libraries.Bolt.Objects.Components.Buttons.Images;
using Libraries.Bolt.Objects.Components.Progress;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data;
using Libraries.Game.Models.Data.Skills;
using Libraries.Sparky.Objects.Components.Cards.Water.Sources;
using Libraries.Sparky.Objects.Groups.You;
using UnityEngine;
using UnityEngine.Serialization;

namespace Libraries.Sparky.Objects.Components.Cards.Water
{
    public class WaterDashboardCard : BaseExtension
    {
        // Fields
        // Private
        private bool UnlockingWaterSource;
        private float UnlockingWaterSourceTime;
        private WaterSourcesDetailView ref_waterSourcesDetailView;
        // Private Serialized
        [FormerlySerializedAs("unlockWaterSourceOldButton")] [SerializeField] private BaseImageButton _unlockWaterSourceButton;
        [FormerlySerializedAs("unlockingWaterSourceBaseProgress")] [SerializeField] private HorizontalBaseProgress _unlockingWaterSourceProgress;
        [FormerlySerializedAs("HydrationGroup")] [FormerlySerializedAs("_waterDashboardHydration")] [SerializeField] private YouHydrationGroup YouHydrationGroup;

        // Properties
        // Private
        // Private Static
        private static WaterData WaterData => DataController.CoreData.Water;
        private static Skill LearningSkill => DataController.CoreData.Skills.Learning;
        private static Skill AnalyzingSkill => DataController.CoreData.Skills.Analyzing;

        // Methods
        // Base Methods
        public void Build(WaterSourcesDetailView waterSourcesDetailView) => 
            ref_waterSourcesDetailView = waterSourcesDetailView;

        public override void OnStart()
        {
            ResetUnlockingWaterSource();
            YouHydrationGroup.OnStart();
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
           
            UpdateUnlockSourceButton();
            UpdateUnlockSourceProgress();
            CompleteUnlockingWaterSource();
            YouHydrationGroup.OnUpdate();
            
            return true;
        }
        
        public override void OnRun()
        {
            if (!UnlockingWaterSource) return;
            
            UnlockingWaterSourceTime += TimeController.DeltaTime;
        }
        
        // Public Methods
        public void StartUnlockingSource()
        {
            if (WaterData.WaterSourceUnlocks[0]) return;
            
            UnlockingWaterSource = true;
        }

        // Private Methods
        private void UpdateUnlockSourceProgress()
        {
            _unlockingWaterSourceProgress.SetActive(!WaterData.HaveUnlockedAllSources());
            if (WaterData.HaveUnlockedAllSources()) return;

            _unlockingWaterSourceProgress.SetProgress(UnlockingWaterSourceTime, 10.0f);
            _unlockingWaterSourceProgress.SetLabel($"Unlocking... {10.0f - UnlockingWaterSourceTime:F1}s");
        }
        
        private void UpdateUnlockSourceButton() => 
            _unlockWaterSourceButton.SetActive(!WaterData.HaveUnlockedAllSources());

        private void CompleteUnlockingWaterSource()
        {
            if (!UnlockingWaterSource || UnlockingWaterSourceTime < 10.0f) return;
            
            WaterData.UnlockWaterSource(0);
            LearningSkill.AddXp(1.0f);
            ref_waterSourcesDetailView.OnUnlockSource();
            ResetUnlockingWaterSource();
        }
        
        private void ResetUnlockingWaterSource()
        {
            UnlockingWaterSource = false;
            UnlockingWaterSourceTime = 0.0f;
            UpdateUnlockSourceProgress();
        }
    }
}