using Libraries.Bolt.Extensions;
using Libraries.Bolt.Objects;
using Libraries.Sparky.Configs.Research;
using Libraries.Sparky.Objects.Components.Cards.Research;
using Libraries.Sparky.Objects.Components.Cards.Theories;

namespace Libraries.Sparky.Objects.Components.Contents
{
    public class ResearchContent : BaseContent
    {
        // Fields
        // Private
        private ResearchTheoriesCard _theories;

        // Properties
        // Private
        private ResearchTheoriesCard Theories => GetComponentInChildren(ref _theories);

        // Methods
        // Base Methods
        public override IObject Build()
        {
            ResearchConfigs.AssignIds();
            Theories.Build();
            
            return this;
        }

        public override void OnStart() => 
            Theories.OnStart();

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            Theories.OnUpdate();

            return true;
        }

        public override void OnRun() => 
            Theories.OnRun();
    }
}
