using Libraries.Bolt.Extensions.Drawers;

namespace Libraries.Sparky.Objects.Components.Cards.Skilling
{
    public class SkillEffectDrawer : DrawerExtension
    {
        // Methods
        // Base
        public void Build(TriggerCallback triggerCallback, string[] names) {
            base.Build(triggerCallback);
            for (var i = 0; i < names.Length; i++) {
                var button = Instantiate(m_drawerButtonPrefab, transform);
                button.OnStart(false, i, Trigger);
                button.SetLabel(names[i]);
                Buttons.Add(button); } }
    }
}