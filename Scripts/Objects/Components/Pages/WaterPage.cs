using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Page;

namespace Libraries.Sparky.Objects.Components.Pages
{
    public class WaterPage : BaseSkillablePage
    {
        private const string PageName = "Water";
        
        public override IObject Build()
        {
            base.Build(PageName);
            
            return this;
        }
    }
}
