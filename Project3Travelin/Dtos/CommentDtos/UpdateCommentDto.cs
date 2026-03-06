namespace Project3Travelin.Dtos.CommantDtos;

public class UpdateCommentDto
{
    public string CommantId { get; set; }
    public string Headline { get; set; }
    public string CommantDetail { get; set; }
    public DateTime CommantDate { get; set; }
    public bool IsStatus { get; set; }
}