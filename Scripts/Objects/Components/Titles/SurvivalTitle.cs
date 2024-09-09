using Libraries.Bolt.Objects.Components.Title;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data;
using Libraries.Game.Models.Data.Skills;
using TMPro;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Titles
{
    public class SurvivalTitle : BasePageSkillableTitle
    {
        // Fields
        // Private
        // Private Serialized
        [SerializeField] private TMP_Text ResourcesText;
        
        // Properties
        // Private
        private SurvivalData Survival => DataController.SurvivalData;
        private Skill SurvivalSkill => DataController.SurvivalSkill;

        // Methods
        // Base Methods
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            ResourcesText.SetText($"{Survival.Fire:N0} Fire");
            LevelChip.SetText(SurvivalSkill.Level.ToString());
            LevelProgress.SetProgress(SurvivalSkill.Xp, SurvivalSkill.GetXpForNextLevel());
            LevelProgress.SetLabel($"{SurvivalSkill.Xp:N0}/{SurvivalSkill.GetXpForNextLevel():N0} XP");

            return true;
        }
    }
}
