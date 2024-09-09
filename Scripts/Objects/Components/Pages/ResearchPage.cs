using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Page;

namespace Libraries.Sparky.Objects.Components.Pages
{
    public class ResearchPage : BaseSkillablePage
    {
        private const string PageName = "Research";
        
        public override IObject Build()
        {
            base.Build(PageName);
            
            return this;
        }
    }
}