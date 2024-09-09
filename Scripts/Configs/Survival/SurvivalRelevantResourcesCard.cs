using System.Collections.Generic;
using Libraries.Bolt.Configs.Components.Chip;
using Libraries.Bolt.Objects;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data;
using Libraries.Sparky.Objects.Components.Cards.Other;
using Mono.Cecil;
using UnityEngine;

namespace Libraries.Sparky.Configs.Survival
{
    public class SurvivalRelevantResourcesCard : RelevantResourcesCard
    {
        // Properties
        // Private
        private YouData You => DataController.CoreData.You;
        private GatheringData Gathering => DataController.CoreData.Gathering;
        private SurvivalData Survival => DataController.CoreData.Survival;

        // Methods
        // Base Methods
        public override IObject Build()
        {
            base.Build(new List<ChipConfig>
            {
                new(color: new Color(0.333f, 0.675f, 0.706f)),
                new(color: new Color(0.675f, 0.646f, 0.000f)),
                new(color: new Color(0.745f, 0.345f, 0.000f)),
            });

            return this;
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            SetLabel(0, $"Hydration\n<size=12>{You.Hydration:N2}L</size>");
            SetLabel(1, $"Twigs\n<size=12>{Gathering.Twigs:N2}</size>");
            SetLabel(2, $"Fire\n<size=12>{Survival.Fire:N2}L</size>");

            return true;
        }
    }
}