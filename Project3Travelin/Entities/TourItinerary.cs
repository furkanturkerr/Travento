using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project3Travelin.Entities;

public class TourItinerary
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ItineraryId { get; set; }

    [BsonElement("TourId")]  // bunu ekle
    public string TourId { get; set; }

    [BsonElement("Days")]    // bunu ekle
    public List<ItineraryDay> Days { get; set; } = new();
}

public class ItineraryDay
{
    public int DayNumber { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}