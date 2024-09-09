using Libraries.Bolt;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects.Components.Buttons.Icon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Libraries.Sparky
{
    [DisallowMultipleComponent]
    public class SparkyHeader : BaseExtension
    {
        // Fields
        // Private
        // Private Serialized
        [FormerlySerializedAs("_moreButton")] [SerializeField] private MoreIconButton MoreIconButton;
        
        // Methods
        // Base Methods
        public override void OnStart() => 
            MoreIconButton.OnStart();
    }
}