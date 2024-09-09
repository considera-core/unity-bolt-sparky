using Libraries.Bolt.Objects.Components.Title;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data;
using Libraries.Game.Models.Data.Skills;
using TMPro;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Titles
{
    public class GatheringTitle : BasePageSkillableTitle
    {
        // Fields
        // Private
        // Private Serialized
        [SerializeField] private TMP_Text ResourcesText;
        
        // Properties
        // Private
        private GatheringData Gathering => DataController.GatheringData;
        private Skill GatheringSkill => DataController.GatheringSkill;

        // Methods
        // Base Methods
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            ResourcesText.SetText(
                $"<color=#AC7600>{Gathering.Twigs:N2} Wood</color> " +
                $"<color=#878787>{Gathering.Rocks:N2} Rocks</color>" +
                $"<color=#878787>{Gathering.Rocks:N2} Sand</color>");
            
            LevelChip.SetText(GatheringSkill.Level.ToString());
            LevelProgress.SetProgress(GatheringSkill.Xp, GatheringSkill.GetXpForNextLevel());
            LevelProgress.SetLabel($"{GatheringSkill.Xp:N0}/{GatheringSkill.GetXpForNextLevel():N0} XP");

            return true;
        }
    }
}
