using Libraries.Bolt.Extensions.UI.Cards;
using Libraries.Bolt.Objects.Components.Buttons.Images;
using Libraries.Bolt.Objects.Components.Progress;
using Libraries.Sparky.Configs.Water;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Libraries.Sparky.Objects.Components.Cards.Water
{
    [DisallowMultipleComponent]
    public class WaterAnalysisCard : CardExtension
    {
        // Fields
        private WaterAnalysisConfig _config;
        // Private
        // Private Serialized
        [FormerlySerializedAs("_description")] [SerializeField] private TMP_Text _descriptionText;
        [FormerlySerializedAs("_volume")] [SerializeField] private TMP_Text _volumeText;
        [SerializeField] private BaseImageButton _analyzeButton;
        [SerializeField] private BaseProgress _analyzeProgress;
        // Private Static
        private static readonly Color Valid = new(0.486f, 0.757f, 0);
        private static readonly Color Invalid = new(0.757f, 0.486f, 0);

        // Methods
        // Base Methods
        public override void OnStart()
        {
            _config = WaterAnalysisConfig.WaterAnalysis;
            _descriptionText.SetText(_config.GetDescription());
            CardHeader.SetHeaderText(_config.Title);
        }
        
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            _descriptionText.SetText(_config.GetDescription());
            _volumeText.SetText(_config.GetVolume());
            _analyzeButton.SetColor(_config.CanAnalyze() && !_config.Analyzing ? Valid : Invalid);
            _analyzeButton.SetText(_config.Analyzing ? "Busy" : "Analyze");
            _analyzeProgress.SetProgress(_config.AnalyzeTime, _config.AnalyzeTimeLimit);
            _analyzeProgress.SetLabel($"Analyzing... {_config.GetTimeRemaining():N1}s");
            
            return true;
        }
        
        public override void OnRun() =>
            _config.Run();
        
        // Public Methods
        public void Analyze()
        {
            if (!_config.CanAnalyze()) return;
            _config.Analyzing = true;
        }
    }
}