using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Page;

namespace Libraries.Sparky.Objects.Components.Pages
{
    public class GatheringPage : BaseSkillablePage
    {
        private const string PageName = "Gathering";
        
        public override IObject Build()
        {
            base.Build(PageName);
            
            return this;
        }
    }
}
