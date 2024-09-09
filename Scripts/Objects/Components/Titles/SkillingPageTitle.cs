using Libraries.Bolt.Objects.Components.Title;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data.Skills;
using TMPro;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Titles
{
    public class SkillingPageTitle : BasePageTitle
    {
        // Fields
        // Private
        // Private Serialized
        [SerializeField] private TMP_Text AccountLevelText;
        
        // Properties
        // Private
        private Skill WaterSkill => DataController.WaterSkill.Water;

        // Methods
        // Base Methods
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            AccountLevelText.SetText($"Account Level: {WaterSkill.Level}");

            return true;
        }
    }
}
