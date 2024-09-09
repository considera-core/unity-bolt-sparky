using System;
using Libraries.Bolt.Objects;
using Libraries.Bolt.Objects.Components.Buttons;
using Libraries.Bolt.Objects.Components.Buttons.Images;
using Libraries.Bolt.Objects.Components.Progress;
using Libraries.Game.Controllers;
using Libraries.Game.Models.Data;

namespace Libraries.Sparky.Objects.Groups.You
{
    public class YouHydrationGroup : BaseObject
    {
        // Fields
        // Private
        private RaisedImageButton _drinkButton;
        private HorizontalBaseProgress _hydrationBaseProgress;
        	
        // Properties
        // Private
        private WaterData Water => DataController.CoreData.Water;
        private YouData You => DataController.CoreData.You;
        private double HydrationAmount => Math.Min(You.GetHydrationLimit() - You.Hydration, Water.Water);

        private RaisedImageButton DrinkButton =>
            GetComponentInChildren(ref _drinkButton);
        
        private HorizontalBaseProgress HydrationBaseProgress =>
            GetComponentInChildren(ref _hydrationBaseProgress);
        
        // Methods
        // Base Methods
        public override void OnStart()
        {
            DrinkButton.Build(trigger: OnDrink);
            HydrationBaseProgress.SetProgress(You.Hydration, You.GetHydrationLimit());
        }

        public override bool OnUpdate()
        {
            if (!base.OnUpdate()) return false;
            
            DrinkButton.SetText($"Drink Water\n-{HydrationAmount:F2}<sprite=\"resourcewater\" index=0>");
            HydrationBaseProgress.SetLabel($"Hydration: {You.Hydration:F2}/{You.GetHydrationLimit():F2}");
            HydrationBaseProgress.SetProgress(You.Hydration, You.GetHydrationLimit());

            return true;
        }
        
        // Private
        private void OnDrink()
        {
            DataController.CoreData.Skills.You.Hydration.AddXp(HydrationAmount);
            DataController.CoreData.You.Hydration += HydrationAmount;
            DataController.CoreData.Water.Water -= HydrationAmount;
        }
    }
}