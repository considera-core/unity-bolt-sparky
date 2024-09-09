using Libraries.Bolt.Extensions.Drawers;
using Libraries.Sparky.Configs.Research;

namespace Libraries.Sparky.Objects.Components.Drawers
{
    public class ResearchTheoriesDrawer : DrawerExtension
    {
        // Methods
        // Base
        public override void Build(TriggerCallback triggerCallback)
        {
            base.Build(triggerCallback);

            foreach (var theory in ResearchConfigs.TheoriesConfigs)
            {
                var button = Instantiate(m_drawerButtonPrefab, transform);
                button.OnStart(false, theory.TheoryId, Trigger);
                button.SetLabel(theory.Name);
                Buttons.Add(button);
            }
        }
    }
}