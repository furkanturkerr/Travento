using Project3Travelin.Entities;

namespace Project3Travelin.Dtos.TourTineraryDtos;

public class ResultTineraryDto
{
    public string ItineraryId { get; set; }
    public string TourId { get; set; }
    public List<ItineraryDay> Days { get; set; } = new();
}