using Libraries.Bolt;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects.Components.Progress;
using Libraries.Game.Models.Data.Skills;
using UnityEngine;
using UnityEngine.Serialization;

namespace Libraries.Sparky.Objects.Components.Cards.Skilling
{
    public class SkillingSummarySkillProgress : BaseExtension
    {
        // Fields
        // Private
        private Skill _skill;
        private string _name;
        // Private Serialized
        [FormerlySerializedAs("baseProgress")] [SerializeField] private VerticalBaseProgress _progress;

        // Methods
        // Base Methods
        public void Build(Skill skill, string name)
        {
            _skill = skill;
            _name = name;
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            UpdateSkillProgress();
            return true;
        }

        // Private Methods
        private void UpdateSkillProgress()
        {
            var unlocked = _skill.Xp > 0 || _skill.Level > 0;
            _progress.SetActive(unlocked);
            
            if (!unlocked) return;

            _progress.SetProgress(_skill.Xp, _skill.GetXpForNextLevel());
            _progress.SetLabel($"{_name}\n<size=16>{_skill.Level}\n{_progress.GetProgressPercentString(2)}</size>");
        }
    }
}