using Libraries.Bolt.Objects.Components.Title;
using Libraries.Game.Controllers;
using Libraries.Game.Enums;
using Libraries.Game.Models.Data;
using Libraries.Game.Models.Data.Skills;
using TMPro;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Titles
{
    public class MiningTitle : BasePageSkillableTitle
    {
        // Fields
        // Private
        // Private Serialized
        [SerializeField] private TMP_Text ResourcesText;
        
        // Properties
        // Private
        private MiningData MiningData => DataController.MiningData;
        private Skill MiningSkill => DataController.MiningSkill;

        // Methods
        // Base Methods
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            ResourcesText.SetText($"<color=#BFBFBF>{MiningData.GetDust(MiningResource.Stone):N2} Dust </color>");
            
            LevelChip.SetText(MiningSkill.Level.ToString());
            LevelProgress.SetProgress(MiningSkill.Xp, MiningSkill.GetXpForNextLevel());
            LevelProgress.SetLabel($"{MiningSkill.Xp:N0}/{MiningSkill.GetXpForNextLevel():N0} XP");
            
            return true;
        }
    }
}
