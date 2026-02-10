namespace Project3Travelin.Dtos.CommantDtos;

public class CreateCommentDto
{
    public string Headline { get; set; }
    public string CommentDetail { get; set; }
    public int Skor { get; set; }
    public DateTime CommentDate { get; set; }
    public bool IsStatus { get; set; }
}