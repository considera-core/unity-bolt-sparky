using System.Linq;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects.Components.Table;
using Libraries.Sparky.Configs.Skills;

namespace Libraries.Sparky.Objects.Components.Cards.Skilling
{
    // look at the other view classes, they are wihin the cards component folder, they need to be moved
    public class SkillEffectTableView : BaseExtension
    {
        private SkillEffectsConfig _config;
        private Table _table;
        
        private Table Table => 
            GetComponentInChildren(ref _table);

        public void Build(SkillEffectsConfig effectConfig) 
        {
            _config = effectConfig;
            Table.Build(SkillsConfigs.SkillEffectTableConfig(_config));
        }

        public override void OnStart()
        {
            var row = 0;
            _config.Effects.ToList().ForEach(x =>
            {
                Table.UpdateBody(0, row, x.Skill);
                Table.UpdateBody(2, row++, x.Name);
            });
        }

        public void UpdateBody(int col, int row, string value) => 
            Table.UpdateBody(col, row, value);
    }
}