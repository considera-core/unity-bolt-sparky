using Libraries.Bolt.Objects.Components.Title;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data;
using Libraries.Game.Models.Data.Skills;
using TMPro;
using UnityEngine;
using static Libraries.Game.Helpers.SpriteStrings;

namespace Libraries.Sparky.Objects.Components.Titles
{
    public class WaterTitle : BasePageSkillableTitle
    {
        // Fields
        // Private
        // Private Serialized
        [SerializeField] private TMP_Text ResourcesText;
        
        // Properties
        // Private
        private static WaterData WaterData => DataController.WaterData;
        private static WaterSkill WaterSkill => DataController.WaterSkill;

        // Methods
        // Base Methods
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            ResourcesText.SetText(
                $"<color=#1CB7FF>{WaterData.Water:N2}L{ResourceWater}</color> " +
                $"<color=#7CC100>{WaterData.WaterPolluted:N2}L{ResourceWaterPolluted}</color>");
            LevelChip.SetText(WaterSkill.Water.Level.ToString());
            LevelProgress.SetProgress(WaterSkill.Water.Xp, WaterSkill.Water.GetXpForNextLevel());
            LevelProgress.SetLabel($"{WaterSkill.Water.Xp:N0}/{WaterSkill.Water.GetXpForNextLevel():N0} XP");

            return true;
        }
    }
}
