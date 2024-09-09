using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Page;

namespace Libraries.Sparky.Objects.Components.Pages
{
    public class SurvivalPage : BaseSkillablePage
    {
        private const string PageName = "Survival";
        
        public override IObject Build()
        {
            base.Build(PageName);
            
            return this;
        }
    }
}
