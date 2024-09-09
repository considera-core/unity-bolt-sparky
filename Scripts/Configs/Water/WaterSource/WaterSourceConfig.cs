namespace Libraries.Sparky.Configs.Water.WaterSource
{
    public class WaterSourceConfig // todo: IPartConfig or IFeatureConfig?
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public WaterSourceExtractionConfig ExtractionConfig { get; set; }
        public WaterSourcePanningConfig PanningConfig { get; set; }
        
        public void Run()
        {
            ExtractionConfig.Run();
            PanningConfig.Run();
        }
    }
}
