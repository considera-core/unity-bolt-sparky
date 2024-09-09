using System.Collections.Generic;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects;
using Libraries.Sparky.Enums.Water;
using Libraries.Sparky.Objects.Components.Buttons;

namespace Libraries.Sparky.Objects.Components.Cards.Water.Sources
{
    public class WaterSourcesCard : BaseExtension
    {
        private List<WaterSourcesIconButton> _headerIconButtons;
        private WaterSourcesTableView _tablePage;
        private WaterSourcesDetailView _detailPage;
        private WaterSourcesGridView _gridPage;

        public WaterSourcesDetailView DetailPage => 
            GetComponentInChildren(ref _detailPage);
        
        private List<WaterSourcesIconButton> HeaderIconButtons =>
            GetComponentsInChildren(ref _headerIconButtons);
        
        private WaterSourcesTableView TablePage =>
            GetComponentInChildren(ref _tablePage);
        
        private WaterSourcesGridView GridPage =>
            GetComponentInChildren(ref _gridPage);

        public override IObject Build()
        {
            HeaderIconButtons.ForEach(x => x.Build(
                active: false, 
                viewType: WaterSourcesViewType.Undefined,
                triggerCallback: TogglePage));
            
            DetailPage.Build();
            TablePage.Build();
            
            return this;
        }

        public override void OnStart()
        {
            TablePage.OnStart();
            DetailPage.OnStart();
            TogglePage(WaterSourcesViewType.Detail);
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            TablePage.OnUpdate();
            DetailPage.OnUpdate();
            
            return true;
        }
        
        public override void OnRun() => 
            DetailPage.OnRun();

        private void TogglePage(WaterSourcesViewType viewType)
        {
            DetailPage.SetActive(viewType == WaterSourcesViewType.Detail);
            TablePage.gameObject.SetActive(viewType == WaterSourcesViewType.Table);
            GridPage.SetActive(viewType == WaterSourcesViewType.Grid);
            HeaderIconButtons.ForEach(x => 
                x.SetButtonActive(x.ViewType == viewType));
        }
    }
}