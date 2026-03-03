using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project3Travelin.Entities;

public class Slider
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SliderId { get; set; }
    public string Title { get; set; }
    public string Image1 { get; set; }
    public string Image2 { get; set; }
    public string Image3 { get; set; }
}