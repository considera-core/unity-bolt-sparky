using Libraries.Bolt.Extensions;
using Libraries.Bolt.Objects;
using Libraries.Sparky.Objects.Components.Cards.Skilling;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Contents
{
    public class SkillingContent : BaseContent
    {
        // Fields
        // Private
        // Private Serialized
        [SerializeField] private SkillingSummaryExtension _skillingSummary;

        // Methods
        // Base Methods
        public override IObject Build()
        {
            _skillingSummary.Build();
            
            return this;
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            _skillingSummary.OnUpdate();

            return true;
        }
    }
}
