using Libraries.Game.Controllers;

namespace Libraries.Sparky.Enums
{
    public enum ResourceType
    {
        Water,
    }

    public static class ResourceTable
    {
        public static double GetResourceValue(ResourceType resourceType)
        {
            return resourceType switch
            {
                ResourceType.Water => DataController.CoreData.Water.Water,
                _ => 0.0
            };
        }
    } 
}