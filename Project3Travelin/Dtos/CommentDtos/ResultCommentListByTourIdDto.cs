namespace Project3Travelin.Dtos.CommantDtos;

public class ResultCommentListByTourIdDto
{
    public string CommentId { get; set; }
    public string Headline { get; set; }
    public string CommentDetail { get; set; }
    public int Skor { get; set; }
    public DateTime CommentDate { get; set; }
    public bool IsStatus { get; set; }
    public string TourId { get; set; }
}