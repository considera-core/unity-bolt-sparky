using Libraries.Bolt.Extensions.UI.Cards;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Buttons.Images;
using Libraries.Bolt.Objects.Components.Progress;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data;
using Libraries.Sparky.Configs.Fire;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Libraries.Sparky.Objects.Components.Cards.Fire
{
    public class FiremakingCard : CardExtension
    {
        // Fields
        // Private
        private FiremakingConfig _config;
        // Private Serialized
        [SerializeField] private Color Valid = new(0.486f, 0.757f, 0);
        [SerializeField] private Color Invalid = new(0.757f, 0.486f, 0);
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [FormerlySerializedAs("igniteOldButton")] [SerializeField] private BaseImageButton _igniteButton;
        [FormerlySerializedAs("igniteBaseProgress")] [SerializeField] private HorizontalBaseProgress _igniteProgress;
        [FormerlySerializedAs("fireBaseProgress")] [SerializeField] private HorizontalBaseProgress _fireProgress;

        // Properties
        // Private
        private SurvivalData Survival => DataController.SurvivalData;
        // Public

        // Methods
        // Base Methods
        public IObject Build(FiremakingConfig config)
        {
            _config = config;
            
            return this;
        }

        public override void OnStart()
        {
            _title.SetText(_config.Title);
            _description.SetText(_config.GetDescription());
            _igniteButton.Build(trigger: Ignite);
            _igniteProgress.SetProgress(0.0f);
            _fireProgress.SetProgress(0.0f);
        }
        
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            _igniteButton.SetColor(_config.CanIgnite() ? Valid : Invalid);
            _igniteButton.SetText(_config.Igniting || _config.Ignited ? "Busy" : "Ignite with Sticks");
            _igniteProgress.SetProgress(_config.GetIgnitePercentage());
            _igniteProgress.SetLabel($"Igniting... {_config.GetIgniteTimeRemaining():N1}s");
            _fireProgress.SetProgress(_config.GetFirePercentage());
            _fireProgress.SetLabel($"Burning... {_config.GetFireTimeRemaining():N1}s");
            
            return true;
        }
        
        public override void OnRun() =>
            _config.Run();
        
        // Public
        public void Ignite() => 
            _config.StartIgnite();
    }
}