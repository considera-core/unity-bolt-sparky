using System;
using System.Collections.Generic;
using Libraries.Bolt.Configs.Components.Table;

namespace Libraries.Sparky.Configs.Skills
{
    public static class SkillsConfigs
    {
        public static SkillEffectConfig WaterChanceSkillEffect = 
            new(
                name: "Water Chance Increase",
                skill: "Water",
                effects: new[]
                {
                    0.00,
                    0.05,
                    0.10,
                    0.14,
                    0.18,
                    0.21,
                    0.24,
                    0.26,
                    0.28,
                    0.29,
                },
                effect: x => x < 10 ? 0 : 0.30 + (x - 10) * 0.01);

        public static SkillEffectConfig WaterAmountSkillEffect =
            new(
                name: "Water Amount Increase",
                skill: "Water",
                effects: new[]
                {
                    0.000,
                    0.010,
                    0.020,
                    0.030,
                    0.040,
                    0.060,
                    0.080,
                    0.100,
                    0.120,
                    0.140,
                    0.160,
                },
                effect: x => x < 10 ? 0 : 0.18 + (x - 10) * 0.001);

        public static SkillEffectsConfig WaterSkillEffects =
            new(effects: new[]
            {
                WaterChanceSkillEffect,
                WaterAmountSkillEffect,
            });
        
        
        private static readonly List<TableColumnConfig> SkillEffectTableColumns = new()
        {
            new TableColumnConfig("Skill", 0.25f),
            new TableColumnConfig("Level", 0.15f),
            new TableColumnConfig("Name", 0.35f),
            new TableColumnConfig("Effect", 0.25f),
        };

        public static TableConfig SkillEffectTableConfig(SkillEffectsConfig config) => 
            new(cols: SkillEffectTableColumns, config.Effects.Length);

        public static string[] WaterRelatedSkills = {"Water", "Water Analysis"};
    }

    public class SkillEffectsConfig
    {
        public SkillEffectConfig[] Effects;
        
        public SkillEffectsConfig(SkillEffectConfig[] effects) => 
            Effects = effects;

        public double GetEffect(int index, int level) =>
            Effects[index].GetEffect(level);

        public string GetEffectString(int index, int level) =>
            $"+{Effects[index].GetEffect(level):N2}";
    }
    

    public class SkillEffectConfig
    {
        private readonly double[] _effects;
        private readonly Func<int, double> _effect;
        
        public string Name { get; }
        public string Skill { get; }

        public SkillEffectConfig(
            string name,
            string skill,
            double[] effects = null, 
            Func<int, double> effect = null)
        {
            Name = name;
            Skill = skill;
            _effects = effects;
            _effect = effect;
        }
        
        public double GetEffect(int index) => 
            _effects?.Length > 0 
                ? _effects.Length > index 
                    ? _effects[index] 
                    : _effect(index)
                : _effect(index);
    }
}