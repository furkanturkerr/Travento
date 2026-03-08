namespace Project3Travelin.Dtos.ContactForm;

public class ResultContactDto
{
    public string ContactId { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string comments { get; set; }
    public DateTime CreateDate { get; set; }

    public bool IsStatus { get; set; }
}