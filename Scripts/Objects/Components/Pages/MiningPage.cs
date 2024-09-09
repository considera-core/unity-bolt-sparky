using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Page;

namespace Libraries.Sparky.Objects.Components.Pages
{
    public class MiningPage : BaseSkillablePage
    {
        private const string PageName = "Mining";
        
        public override IObject Build()
        {
            base.Build(PageName);
            
            return this;
        }
    }
}
