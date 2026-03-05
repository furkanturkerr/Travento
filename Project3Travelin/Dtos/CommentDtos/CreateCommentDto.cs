namespace Project3Travelin.Dtos.CommantDtos;

public class CreateCommentDto
{
    public string Headline { get; set; }
    public string CommentDetail { get; set; }
    public string TourId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Cleanliness { get; set; }
    public int Facilities { get; set; }
    public int ValueForMoney { get; set; }
    public int Service { get; set; }
    public int Location { get; set; }
    public DateTime CommentDate { get; set; } = DateTime.Now;
    public bool IsStatus { get; set; } = true;
}