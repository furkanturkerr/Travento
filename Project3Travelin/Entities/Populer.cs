using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project3Travelin.Entities;

public class Populer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string PopulerId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string ImageUrl { get; set; }
}