using Libraries.Bolt;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Sparky.Objects.Groups.You;
using UnityEngine;
using UnityEngine.Serialization;

namespace Libraries.Sparky.Objects.Components.Cards.Gathering
{
    public class GatheringDashboardExtension : BaseExtension
    {
        // Fields
        // Private Serialized
        [FormerlySerializedAs("HydrationGroup")] [FormerlySerializedAs("_waterDashboardHydration")] [SerializeField] private YouHydrationGroup YouHydrationGroup;

        // Properties
        // Private
        // Private Static

        // Methods
        // Base Methods
        public override void OnStart() => 
            YouHydrationGroup.OnStart();

        public override bool OnUpdate() => 
            YouHydrationGroup.OnUpdate();
    }
}