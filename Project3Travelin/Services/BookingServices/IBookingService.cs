using Project3Travelin.Dtos.BookingDtos;

namespace Project3Travelin.Services.BookingServices;

public interface IBookingService
{
    Task<List<ResultBookingDto>> GetAllBookingAsync();
    Task UpdateBookingAsync(UpdateBookingDto updateBookingDto);
    Task CreateBookingAsync(CreateBookingDto createBookingDto);
    Task DeleteBookingAsync(string id);
    Task Approve(string id);
    Task<GetBookingByIdDto> GetBookingByIdAsync(string id);
}