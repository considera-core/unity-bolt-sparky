using Libraries.Bolt.Extensions;
using Libraries.Bolt.Objects;
using Libraries.Sparky.Configs.Gathering;
using Libraries.Sparky.Objects.Components.Cards.Gathering;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Contents
{
    public class GatheringContent : BaseContent
    {
        // Fields
        // Private
        // Private Serialized
        [SerializeField] private GatheringDashboardExtension _dashboard;
        [SerializeField] private GatheringPlaceExtension _twigGatheringPlace;
        [SerializeField] private GatheringPlaceExtension _rockGatheringPlace;
        [SerializeField] private GatheringPlaceExtension _sandGatheringPlace;

        // Properties
        // Private

        // Methods
        // Base Methods
        public override IObject Build()
        {
            _twigGatheringPlace.Build(GatheringPlaceConfig.GatheringTwigsPlace);
            _rockGatheringPlace.Build(GatheringPlaceConfig.GatheringRocksPlace);
            _sandGatheringPlace.Build(GatheringPlaceConfig.GatheringSandPlace);

            return this;
        }
        
        public override void OnStart()
        {
            _dashboard.OnStart();
            _twigGatheringPlace.OnStart();
            _rockGatheringPlace.OnStart();
            _sandGatheringPlace.OnStart();
        }
        
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            _dashboard.OnUpdate();
            _twigGatheringPlace.OnUpdate();
            _rockGatheringPlace.OnUpdate();
            _sandGatheringPlace.OnUpdate();

            return true;
        }
        
        public override void OnRun()
        {
            _twigGatheringPlace.OnRun();
            _rockGatheringPlace.OnRun();
            _sandGatheringPlace.OnRun();
        }
    }
}
