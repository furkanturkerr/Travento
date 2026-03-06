using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project3Travelin.Entities;

public class Contact
{ 
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ContactId { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string comments { get; set; }
}