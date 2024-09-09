using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Page;

namespace Libraries.Sparky.Objects.Components.Pages
{
    public class SkillingPage : BasePage
    {
        private const string PageName = "Skilling";
        
        public override IObject Build()
        {
            base.Build(PageName);
            
            return this;
        }
    }
}
