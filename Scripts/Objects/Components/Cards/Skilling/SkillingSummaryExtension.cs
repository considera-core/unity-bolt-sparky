using System.Collections.Generic;
using System.Linq;
using Libraries.Bolt;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Card;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data.Skills;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Cards.Skilling
{
    public class SkillingSummaryExtension : BaseExtension
    {
        // Fields
        // Private
        private List<SkillingSummarySkillProgress> _skillProgresses;
        private CardBody _cardBodyRef;
        // Private Serialized
        [SerializeField] private SkillingSummarySkillProgress _skillProgress; // todo replace with prefab manager

        // Properties
        // Private
        private CardBody CardBody => _cardBodyRef ??= GetComponentInChildren<CardBody>(true);
        // Private Static
        private static SkillsData Skills => DataController.CoreData.Skills;
        private static Skill WaterSkill => Skills.Water.Water;
        private static Skill LearningSkill => Skills.Learning;
        private static Skill AnalyzingSkill => Skills.Analyzing;

        // Methods
        // Base Methods
        public override IObject Build()
        {
            _skillProgresses = Skills
                .GetSkills()
                .Select(x =>
                {
                    var ins = Instantiate(_skillProgress, CardBody.Padding.transform);
                    ins.Build(x, x.Type.ToString());
                    return ins;
                })
                .ToList();
            
            return this;
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            _skillProgresses.ForEach(x => x.OnUpdate());
            return false;
        }
    }
}