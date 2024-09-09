using System.Collections.Generic;
using Libraries.Bolt;
using Libraries.Bolt.Extensions.UI.Cards;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Buttons.Icon;
using Libraries.Sparky.Configs.Skills;
using Libraries.Sparky.Enums.Skills;
using Libraries.Sparky.Objects.Components.Buttons;
using UnityEngine;

namespace Libraries.Sparky.Objects.Components.Cards.Skilling
{
    public class RelevantSkillsCard : CardExtension
    {
        private int _currentSkill;
        private SkillEffectsConfig[] _relevantConfigs;
        private BaseObject _detailView;
        private SkillEffectTableView _tableView;
        private SkillEffectDrawer _drawer;
        private List<RelevantSkillsIconButton> _headerIconButtons;
        
        protected SkillEffectTableView TableView =>
            GetComponentInChildren(ref _tableView);
        private BaseObject DetailView => 
            GetComponentInChildren(ref _detailView);
        private SkillEffectDrawer Drawer => 
            GetComponentInChildren(ref _drawer);
        private List<RelevantSkillsIconButton> HeaderIconButtons =>
            GetComponentsInChildren(ref _headerIconButtons);

        protected void Build(SkillEffectsConfig[] relevantConfigs) 
        {            
            _relevantConfigs = relevantConfigs;
            
            HeaderIconButtons.ForEach(x => x.Build(
                active: false, 
                viewType: RelevantSkillsViewType.Undefined, 
                triggerCallback: TogglePage));
            
            Card.Build(title: "Related Skills");
            TableView.Build(_relevantConfigs[0]);
            Drawer.Build(SelectSkill, SkillsConfigs.WaterRelatedSkills); 
        }

        public override void OnStart() 
        {
            TableView.OnStart();
            //_detailView.OnStart();
            TogglePage(RelevantSkillsViewType.Table); 
        }

        protected SkillEffectsConfig GetSkillEffectsConfig() =>
            GetSkillEffectsConfig(_currentSkill);

        private SkillEffectsConfig GetSkillEffectsConfig(int index) =>
            _relevantConfigs[index];

        private void SelectSkill(int index) 
        {
            if (index >= _relevantConfigs.Length) return;
            TableView.Build(_relevantConfigs[index]);
            _currentSkill = index; 
        }
        
        private void TogglePage(RelevantSkillsViewType viewType) 
        {
            //_detailView.SetActive(viewType == RelevantSkillsViewType.Detail);
            TableView.gameObject.SetActive(viewType == RelevantSkillsViewType.Table);
            HeaderIconButtons.ForEach(x => x.SetButtonActive(x.ViewType == viewType)); 
        }
    }
}