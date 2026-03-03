using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project3Travelin.Entities;

public class Banner
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string BannerId { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Description { get; set; }
    public string VideoUrl { get; set; }
    public string ImageUrl { get; set; }
}