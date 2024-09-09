using Libraries.Bolt.Extensions.UI.Cards;
using Libraries.Sparky.Configs.Research;
using TMPro;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Cards.Theories
{
    public class ResearchDescriptionCard : CardExtension
    {
        // Fields
        // Private
        // Private Serialized
        [SerializeField] private TMP_Text _description;
        
        // Methods
        // Base
        public void Build(ResearchDescriptionConfig config) => 
            _description.text = config.Description;
    }
}