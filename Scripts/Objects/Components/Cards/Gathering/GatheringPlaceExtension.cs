using Libraries.Bolt.Extensions.UI.Cards;
using Libraries.Bolt.Objects.Components.Button;
using Libraries.Bolt.Objects.Components.Buttons.Images;
using Libraries.Bolt.Objects.Components.Progress;
using Libraries.Sparky.Configs.Gathering;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Libraries.Sparky.Objects.Components.Cards.Gathering
{
    public class GatheringPlaceExtension : CardExtension
    {
        // Fields
        private GatheringPlaceConfig _config;
        // Private
        // Private Serialized
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [FormerlySerializedAs("gatherOldButton")] [SerializeField] private BaseImageButton _gatherButton;
        [FormerlySerializedAs("gatherBaseProgress")] [SerializeField] private BaseProgress _gatherProgress;
        // Private Static
        private static readonly Color Valid = new(0.486f, 0.757f, 0);
        private static readonly Color Invalid = new(0.757f, 0.486f, 0);

        // Methods
        // Base Methods
        public void Build(GatheringPlaceConfig config) => 
            _config = config;

        public override void OnStart()
        {
            _title.SetText(_config.Title);
            _description.SetText(_config.GetDescription());
            _gatherButton.Build(trigger: Gather);
        }
        
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            _description.SetText(_config.GetDescription());
            _gatherButton.SetColor(_config.CanGather() && !_config.Gathering ? Valid : Invalid);
            _gatherButton.SetText(_config.Gathering ? "Busy" : "Gather");
            _gatherProgress.SetProgress(_config.GatherTime, _config.GatherTimeLimit);
            _gatherProgress.SetLabel($"Analyzing... {_config.GetTimeRemaining():N1}s");
            
            return true;
        }
        
        public override void OnRun() =>
            _config.Run();
        
        // Public
        public void Gather()
        {
            if (!_config.CanGather()) return;
            _config.Gathering = true;
        }
    }
}