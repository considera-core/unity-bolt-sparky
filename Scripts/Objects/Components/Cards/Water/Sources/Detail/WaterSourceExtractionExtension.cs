using Libraries.Bolt.Extensions.UI.Cards;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Progress;
using Libraries.Game.Controllers;
using Libraries.Sparky.Configs.Water.WaterSource;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Libraries.Sparky.Objects.Components.Cards.Water.Sources.Detail
{
    public class WaterSourceExtractionExtension : CardExtension
    {
        // Fields
        // Private
        private WaterSourceExtractionConfig _config;
        // Private Serialized
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Button _extractButton;
        [SerializeField] private TMP_Text _extractButtonText;
        [FormerlySerializedAs("_tankProgress")] [SerializeField] private BaseProgress tankBaseProgress;
        [FormerlySerializedAs("_extractProgress")] [SerializeField] private BaseProgress extractBaseProgress;
        // Private Static
        private static readonly Color Valid = new(0.333f, 0.616f, 0.706f);
        private static readonly Color Invalid = new(0.757f, 0.486f, 0.000f);
        
        // Methods
        // Base Methods
        public IObject Build(WaterSourceExtractionConfig config)
        {   
            Card.Build("Extraction", true);
            
            _config = config;
            DataController.WaterData.WaterSources[_config.Id] = _config.Capacity;

            return this;
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            _description.SetText(_config.GetDescription());
            _extractButtonText.SetText(_config.Extracting ? "Pause" : "Extract");
            _extractButton.image.color = _config.Extracting ? Invalid : Valid;
            tankBaseProgress.SetLabel(_config.GetTankLabel(tankBaseProgress.GetProgressPercent()));
            tankBaseProgress.SetProgress(DataController.WaterData.WaterSources[_config.Id], _config.Capacity);
            extractBaseProgress.SetLabel(_config.GetExtractLabel());
            extractBaseProgress.SetProgress(_config.ExtractTime, _config.ExtractTimeLimit);

            return true;
        }

        // Public Methods
        public void ToggleExtract() => 
            _config.Extracting = !_config.Extracting;
    }
}