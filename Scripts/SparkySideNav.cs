using System.Collections.Generic;
using System.Linq;
using Libraries.Bolt.Extensions;
using Libraries.Bolt.Extensions.UI;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Buttons.Drawer;
using Libraries.Bolt.Objects.Components.Drawer;
using Libraries.Game.Controllers;
using Libraries.Game.Enums.Types;
using Libraries.Game.Models.Data.Research;
using UnityEngine;

namespace Libraries.Sparky
{
    [DisallowMultipleComponent]
    public class SparkySideNav : BaseExtension
    {
        // Fields
        // Private
        private SideNavDrawerAnimated _sideNavDrawer;
        private SparkyPages _sparkyPagesRef;
        // Private Serialized
        [SerializeField] private SideAnimatedDrawerButton _waterButton;
        [SerializeField] private SideAnimatedDrawerButton _miningButton;
        [SerializeField] private SideAnimatedDrawerButton _skillingButton;
        [SerializeField] private SideAnimatedDrawerButton _gatheringButton;
        [SerializeField] private SideAnimatedDrawerButton _survivalButton;
        [SerializeField] private SideAnimatedDrawerButton _researchButton;

        [SerializeField] private SideAnimatedDrawerButton _choppingButton;
        [SerializeField] private SideAnimatedDrawerButton _smeltingButton;
        [SerializeField] private SideAnimatedDrawerButton _craftingButton;
        [SerializeField] private SideAnimatedDrawerButton _brewingButton;
        [SerializeField] private SideAnimatedDrawerButton _labButton;
        [SerializeField] private SideAnimatedDrawerButton _acceleratorsButton;
        

        // Properties
        // Private
        private SideNavDrawerAnimated SideNavDrawer => 
            _sideNavDrawer = _sideNavDrawer == null 
                ? GetComponent<SideNavDrawerAnimated>()
                : _sideNavDrawer;
        
        private List<ResearchTheoryData> Theories => 
            DataController.CoreData.Research.Theories;
        
        // Methods
        // Base
        public IObject Build(SparkyPages sparkyPages)
        {
            SideNavDrawer.Build(2.0f, TogglePage);
            _sparkyPagesRef = sparkyPages;

            return this;
        }

        public override void OnStart()
        {
            SideNavDrawer.OnStart();
            _waterButton.OnStart(true, PageType.Water, TogglePage);
            _miningButton.OnStart(false, PageType.Mining, TogglePage);
            _skillingButton.OnStart(false, PageType.Skilling, TogglePage);
            _gatheringButton.OnStart(false, PageType.Gathering, TogglePage);
            _survivalButton.OnStart(false, PageType.Survival, TogglePage);
        }
        
        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            SideNavDrawer.OnUpdate();
            _waterButton.SetActive(IsWaterUnlocked());

            return true;
        }

        // Private
        private void TogglePage(PageType page)
        {
            _sparkyPagesRef.SetPageActive(page);
            SideNavDrawer.DeactivateAllButtons();
            
            switch (page)
            {
                case PageType.Water: _waterButton.SetButtonActive(true); break;
                case PageType.Mining: _miningButton.SetButtonActive(true); break;
                case PageType.Skilling: _skillingButton.SetButtonActive(true); break;
                case PageType.Gathering: _gatheringButton.SetButtonActive(true); break;
                case PageType.Survival: _survivalButton.SetButtonActive(true); break;
                case PageType.Research: _researchButton.SetButtonActive(true); break;
                default: case PageType.Undefined: break;
            }
        }

        private bool IsWaterUnlocked()
        {
            if (Theories.Count < 2) return false;
            
            var filtered = Theories[1].Statements
                .Where(x => x.Statement == "W")
                .ToList();
            
            return filtered.Any() && filtered.First().Proven;
        }
    }
}