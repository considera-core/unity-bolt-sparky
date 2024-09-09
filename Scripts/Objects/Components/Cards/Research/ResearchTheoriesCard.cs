using System.Linq;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data.Research;
using Libraries.Sparky.Configs.Research;
using Libraries.Sparky.Objects.Components.Cards.Theories;
using Libraries.Sparky.Objects.Components.Drawers;

namespace Libraries.Sparky.Objects.Components.Cards.Research
{
    public class ResearchTheoriesCard : BaseExtension
    {
        // Fields
        // Private
        private ResearchDescriptionCard _description;
        private ResearchStatementsCard _statements;
        private ResearchTheoremsCard _theorems;
        private ResearchProvingCard _proving;
        private ResearchTheoriesDrawer _drawer;
        // Private Serialized
        
        // Properties
        // Private
        private ResearchDescriptionCard Description => GetComponentInChildren(ref _description);
        private ResearchStatementsCard Statements => GetComponentInChildren(ref _statements);
        private ResearchTheoremsCard Theorems => GetComponentInChildren(ref _theorems);
        private ResearchProvingCard Proving => GetComponentInChildren(ref _proving);
        private ResearchTheoriesDrawer Drawer => GetComponentInChildren(ref _drawer);
        // Private Static
        private static ResearchData Research => DataController.CoreData.Research;
        
        // Methods
        // Base
        public override IObject Build()
        {
            ViewTheory(0);
            Drawer.Build(ViewTheory);
            
            ResearchConfigs.TheoriesConfigs.ToList().ForEach(x =>
            {
                x.StatementsConfig.Statements.ToList().ForEach(y =>
                {
                    y.SubscribeToOnProved(UpdateDrawerButtons);
                });
            });

            return this;
        }
        
        public override void OnStart()
        {
            Statements.OnStart();
            Theorems.OnStart();
            UpdateDrawerButtons();
        }
        
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            Statements.OnUpdate();
            Theorems.OnUpdate();
            Proving.OnUpdate();
            
            return true;
        }

        public override void OnRun()
        {
            Proving.OnRun();
        }

        // Private
        private void ViewTheory(int theoryIndex)
        {
            Description.Build(ResearchConfigs.TheoriesConfigs[theoryIndex].DescriptionConfig);
            Statements.Build(ResearchConfigs.TheoriesConfigs[theoryIndex].StatementsConfig);
            Statements.OnStart();
            Theorems.Build(ResearchConfigs.TheoriesConfigs[theoryIndex].TheoremsConfig);
            Theorems.OnStart();
            Proving.Build(ResearchConfigs.TheoriesConfigs[theoryIndex]);
        }

        private void UpdateDrawerButtons() =>
            Drawer.Buttons
                .ForEach(x => x.SetActive(x.Id <= Research.GetHighestTheoryId()));
    }
}