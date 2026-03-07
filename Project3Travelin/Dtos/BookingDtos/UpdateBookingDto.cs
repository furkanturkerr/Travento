namespace Project3Travelin.Dtos.BookingDtos;

public class UpdateBookingDto
{
    public string BookingId { get; set; }
    public string TourId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string TcNo { get; set; }
    public int PersonCount { get; set; }
    public DateTime BookingDate { get; set; }
    public string Note { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsConfirmed { get; set; } = false;
}