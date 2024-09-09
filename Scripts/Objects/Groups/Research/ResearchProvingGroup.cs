using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Buttons.Images;
using Libraries.Bolt.Objects.Components.Progress;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data.Research;
using Libraries.Sparky.Configs.Research;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Libraries.Sparky.Objects.Groups.Research
{
    // todo: make color objects so that a card can getcomponents in child with like an AccentColorComponent and just set the color
    public class ResearchProvingGroup : BaseObject
    {
        // Fields
        // Private
        private ResearchStatementConfig _config;
        private BaseImageButton _button;
        private HorizontalBaseProgress _baseProgress;
        // Private Serialized
        [SerializeField] private GameObject _proved;
        [SerializeField] private TMP_Text _requirementsLabel;
        
        // Properties
        // Private
        private ResearchData Research => 
            DataController.CoreData.Research;
        
        private BaseImageButton OldButton => 
            _button ??= GetComponentInChildren<BaseImageButton>();
        
        private HorizontalBaseProgress BaseProgress => 
            _baseProgress ??= GetComponentInChildren<HorizontalBaseProgress>();
        
        // Methods
        // Base
        public IObject Build(ResearchStatementConfig config)
        {
            _config = config;
            OldButton.Build(trigger: _config.Prove);
            OldButton.SetText($"Prove {_config.Statement} is TRUE");

            return this;
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;

            if (!_proved.activeSelf)
            {
                if (BaseProgress.IsActive())
                {
                    BaseProgress.SetProgress(_config.GetProofProgress());
                    BaseProgress.SetLabel($"Proving... {_config.GetTimeRemaining():F2}s");
                }
            
                if (_requirementsLabel.gameObject.activeSelf)
                    _requirementsLabel.SetText(_config.GetDependencyStatus());
            
                BaseProgress.SetActive(!_config.IsProved());
                OldButton.SetActive(!_config.IsProved());
                _requirementsLabel.gameObject.SetActive(!_config.IsProved());
            }
            
            _proved.SetActive(_config.IsProved());

            return true;
        }

        public override void OnRun() => 
            _config.Run();
    }
}