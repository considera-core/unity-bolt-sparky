using Libraries.Bolt.Extensions;
using Libraries.Bolt.Objects;
using Libraries.Sparky.Configs.Fire;
using Libraries.Sparky.Configs.Survival;
using Libraries.Sparky.Objects.Components.Cards.Fire;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Contents
{
    public class SurvivalContent : BaseContent
    {
        // Fields
        // Private
        // Private Serialized
        [SerializeField] private FiremakingCard _firemaking;
        [SerializeField] private SurvivalRelevantResourcesCard _relevantResources;

        // Properties
        // Private

        // Methods
        // Base Methods
        public override IObject Build()
        {
            _firemaking.Build(FiremakingConfig.Firemaking);
            _relevantResources.Build();

            return this;
        }

        public override void OnStart() => 
            _firemaking.OnStart();
        
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            _firemaking.OnUpdate();
            _relevantResources.OnUpdate();

            return true;
        }

        public override void OnRun() =>
            _firemaking.OnRun();
    }
}
