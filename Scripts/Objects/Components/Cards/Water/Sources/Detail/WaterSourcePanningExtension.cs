using Libraries.Bolt;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Chip;
using Libraries.Bolt.Objects.Components.Progress;
using Libraries.Game.Enums.Extensions;
using Libraries.Sparky.Configs.Water.WaterSource;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Libraries.Sparky.Objects.Components.Cards.Water.Sources.Detail
{
    public class WaterSourcePanningExtension : BaseExtension
    {
        // Fields
        // Private
        private WaterSourcePanningConfig _config;
        // Private Serialized
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private ChipComponent _equipped;
        [SerializeField] private Button _beginButton;
        [SerializeField] private TMP_Text _beginButtonText;
        [FormerlySerializedAs("_panningProgress")] [SerializeField] private BaseProgress panningBaseProgress;
        // Private Static
        private static readonly Color Valid = new(0.550f, 0.600f, 0.800f);
        private static readonly Color Invalid = new(0.757f, 0.486f, 0.000f);
        
        // Methods
        // Base Methods
        public IObject Build(WaterSourcePanningConfig config)
        {
            _config = config;
            _title.text = "Panning";
            
            return this;
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            _description.SetText(_config.GetDescription());
            _equipped.SetText(_config.ToolTypeType.ToName());
            _beginButtonText.SetText(_config.Panning ? "Pause" : "Begin");
            _beginButton.image.color = _config.Panning ? Invalid : Valid;
            panningBaseProgress.SetLabel(_config.GetPanningLabel());
            panningBaseProgress.SetProgress(_config.PanningTime, _config.PanningTimeLimit);

            return true;
        }

        // Public Methods
        public void TogglePanning() => 
            _config.Panning = !_config.Panning;
    }
}