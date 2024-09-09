using Libraries.Bolt.Objects;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data.Skills;
using Libraries.Sparky.Configs.Skills;
using Libraries.Sparky.Objects.Components.Cards.Skilling;

namespace Libraries.Sparky.Objects.Components.Cards.Water
{
    public class WaterRelevantSkillsCard : RelevantSkillsCard
    {
        private Skill WaterSkill => 
            DataController.SkillsData.Water.Water;

        private SkillEffectsConfig CurrentConfig => 
            GetSkillEffectsConfig();

        public override IObject Build()
        {
            Build(new[]
            {
                SkillsConfigs.WaterSkillEffects
            });
            
            return this;
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            TableView.UpdateBody(1, 0, WaterSkill.Level.ToString());
            TableView.UpdateBody(3, 0, CurrentConfig.GetEffectString(0, WaterSkill.Level));
            TableView.UpdateBody(1, 1, WaterSkill.Level.ToString());
            TableView.UpdateBody(3, 1, CurrentConfig.GetEffectString(1, WaterSkill.Level));

            return true;
        }
    }
}