using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project3Travelin.Entities;

public class About
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    private string AboutId { get; set; }
    public string Title { get; set; }
    public string AboutDetail { get; set; }
    public string Image1 { get; set; }
    public string Image2 { get; set; }
    public string ImageTitle { get; set; }
}