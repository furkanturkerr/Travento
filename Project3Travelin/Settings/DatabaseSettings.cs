namespace Project3Travelin.Settings;

public class DatabaseSettings : IDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string TourCollectionName { get; set; }
    public string CommentCollectionName { get; set; }
    public string CategoryCollectionName { get; set; }
    public string TourItineraryCollectionName { get; set; }
    public string SliderCollectionName { get; set; }
    public string PopulerCollectionName { get; set; }
    public string AboutCollectionName { get; set; }
    public string BannerCollectionName { get; set; }
    public string FaqCollectionName { get; set; }
}