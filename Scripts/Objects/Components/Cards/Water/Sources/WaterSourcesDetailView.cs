using System.Collections.Generic;
using System.Linq;
using Libraries.Bolt;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Button.Header;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data;
using Libraries.Sparky.Configs.Water;
using Libraries.Sparky.Objects.Components.Cards.Water.Sources.Detail;
using TMPro;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Cards.Water.Sources
{
    public class WaterSourcesDetailView : BaseExtension
    {
        private int _selectedSource;
        private List<HeaderTabButton> _headerTabs;
        private WaterSourceExtractionExtension _extractionDetailPage;
        private WaterSourcePanningExtension _panningDetailPage;
        private RectTransform _headerTabContainer;
        [SerializeField] private HeaderTabButton _headerTabPrefab;
        [SerializeField] private TMP_Text _lockedText;
        
        private WaterSourceExtractionExtension ExtractionDetailPage => 
            GetComponentInChildren(ref _extractionDetailPage);
        
        private WaterSourcePanningExtension PanningDetailPage => 
            GetComponentInChildren(ref _panningDetailPage);
        
        private RectTransform HeaderTabContainer =>
            GetComponentInParent(ref _headerTabContainer);
        
        private WaterData WaterData => 
            DataController.WaterData;
        
        public override IObject Build()
        {
            _headerTabs = new List<HeaderTabButton>();
            
            ExtractionDetailPage.Build(WaterConfigs.WaterSources[_selectedSource].ExtractionConfig);
            PanningDetailPage.Build(WaterConfigs.WaterSources[_selectedSource].PanningConfig);

            if (HeaderTabContainer != null)
                _headerTabs.AddRange(WaterConfigs.WaterSources.Select(x =>
                    (HeaderTabButton)Instantiate(_headerTabPrefab, HeaderTabContainer)
                        .Build(x.Id, false, x.ShortTitle, OnChangeSourceView)));

            return this;
        }
        
        public override void OnStart()
        {
            OnChangeSourceView(0); // todo enum this bitch
            OnUnlockSource();
        }

        public override bool OnUpdate()
        {
            if (base.OnUpdate()) return false;
            
            ExtractionDetailPage.OnUpdate();
            PanningDetailPage.OnUpdate();
            
            return true;
        }

        public override void OnRun() => 
            WaterConfigs.WaterSources[_selectedSource].Run();
        
        public void OnUnlockSource()
        {
            UpdateHeaderTabs();
            UpdateLocked();
        }

        private void OnChangeSourceView(int index)
        {
            var config = WaterConfigs.WaterSources[index];
            _selectedSource = config.Id;
            ExtractionDetailPage.Build(config.ExtractionConfig);
            PanningDetailPage.Build(config.PanningConfig);
            _headerTabs.ForEach(x => x.SetButtonActive(x.Id == config.Id));
        }
        
        private void UpdateHeaderTabs()
        {
            var i = 0;
            _headerTabs.ForEach(x =>
            {
                x.SetActive(i < WaterData.WaterSourceUnlocks.Count && WaterData.WaterSourceUnlocks[i]);
                i++;
            });
        }

        private void UpdateLocked()
        {
            var unlocked = WaterData.WaterSourceUnlocks.Any(x => x);
            _lockedText.gameObject.SetActive(!unlocked);
            ExtractionDetailPage.SetActive(unlocked);
            PanningDetailPage.SetActive(unlocked);
        }

    }
}