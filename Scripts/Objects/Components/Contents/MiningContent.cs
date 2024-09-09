using Libraries.Bolt.Extensions;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data;
using Libraries.Sparky.Objects.Components.Cards.Mining;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Contents
{
    public class MiningContent : BaseContent
    {
        // Fields
        // Private
        // Private Serialized
        [SerializeField] private MiningDashboardCard _dashboard;
        // Properties
        // Private
        private MiningData MiningData => DataController.MiningData;
        
        // Methods
        // Base Methods
        public override void OnStart() => 
            _dashboard.OnStart();

        public override bool OnUpdate() => 
            _dashboard.OnUpdate();

        public override void OnRun() => 
            _dashboard.OnRun();
    }
}
