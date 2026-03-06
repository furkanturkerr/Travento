namespace Project3Travelin.Dtos.CommantDtos;

public class ResultCommentDto
{
    public string CommentId { get; set; }
    public string Headline { get; set; }
    public string CommentDetail { get; set; }
    public DateTime CommentDate { get; set; }
    public bool IsStatus { get; set; }
}