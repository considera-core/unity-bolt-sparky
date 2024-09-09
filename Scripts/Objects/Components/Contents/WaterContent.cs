using Libraries.Bolt.Extensions;
using Libraries.Bolt.Objects;
using Libraries.Sparky.Configs.Skills;
using Libraries.Sparky.Configs.Water.WaterSource;
using Libraries.Sparky.Objects.Components.Cards.Skilling;
using Libraries.Sparky.Objects.Components.Cards.Water;
using Libraries.Sparky.Objects.Components.Cards.Water.Sources;

namespace Libraries.Sparky.Objects.Components.Contents
{
    public class WaterContent : BaseContent
    {
        private WaterSourcesCard _sources;
        private WaterAnalysisCard _analysis;
        private WaterDashboardCard _dashboard;
        private WaterRelevantSkillsCard _waterRelevantSkills; // maybe this will just be a skill card in the future where its more than just the effect table (tabs like the source card header?)

        private WaterSourcesCard Sources => 
            GetComponentInChildren(ref _sources);
        private WaterAnalysisCard Analysis => 
            GetComponentInChildren(ref _analysis);
        private WaterDashboardCard Dashboard => 
            GetComponentInChildren(ref _dashboard);
        private WaterRelevantSkillsCard WaterRelevantSkills => 
            GetComponentInChildren(ref _waterRelevantSkills);

        public override IObject Build()
        {
            Sources.Build();
            Dashboard.Build(Sources.DetailPage);
            WaterRelevantSkills.Build();

            return this;
        }
        
        public override void OnStart()
        {
            Sources.OnStart();
            Analysis.OnStart();
            Dashboard.OnStart();
            WaterRelevantSkills.OnStart();
        }
        
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            Sources.OnUpdate();
            Analysis.OnUpdate();
            Dashboard.OnUpdate();
            WaterRelevantSkills.OnUpdate();

            return true;
        }
        
        public override void OnRun()
        {
            Sources.OnRun();
            Analysis.OnRun();
            Dashboard.OnRun();
        }
    }
}
