using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.BookingServices;

public class BookingService : IBookingService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Booking> _bookingCollection;
    private readonly IMongoCollection<Tour> _tourCollection;

    public BookingService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        _bookingCollection = database.GetCollection<Booking>(databaseSettings.BookingCollectionName);
        _tourCollection = database.GetCollection<Tour>(databaseSettings.TourCollectionName);

        _mapper = mapper;
    }

    public async Task<List<ResultBookingDto>> GetAllBookingAsync()
    {
        var bookings = await _bookingCollection
            .Find(x => true)
            .SortByDescending(x => x.CreatedAt)
            .ToListAsync();

        var tours = await _tourCollection.Find(x => true).ToListAsync();

        var result = bookings.Select(booking =>
        {
            var dto = _mapper.Map<ResultBookingDto>(booking);

            var tour = tours.FirstOrDefault(x => x.TourId == booking.TourId);
            dto.TourTitle = tour?.Title ?? "Tur bulunamadı";

            return dto;
        }).ToList();

        return result;
    }

    public async Task UpdateBookingAsync(UpdateBookingDto updateBookingDto)
    {
        var values = _mapper.Map<Booking>(updateBookingDto);
        await _bookingCollection.FindOneAndReplaceAsync(
            x => x.BookingId == updateBookingDto.BookingId,
            values);
    }

    public async Task CreateBookingAsync(CreateBookingDto createBookingDto)
    {
        var values = _mapper.Map<Booking>(createBookingDto);
        await _bookingCollection.InsertOneAsync(values);
    }

    public async Task DeleteBookingAsync(string id)
    {
        await _bookingCollection.DeleteOneAsync(x => x.BookingId == id);
    }

    public async Task Approve(string id)
    {
        var filter = Builders<Booking>.Filter.Eq(x => x.BookingId, id) &
                     Builders<Booking>.Filter.Eq(x => x.IsConfirmed, false);

        var update = Builders<Booking>.Update.Set(x => x.IsConfirmed, true);

        await _bookingCollection.UpdateOneAsync(filter, update);
    }

    public async Task<GetBookingByIdDto> GetBookingByIdAsync(string id)
    {
        var value = await _bookingCollection.Find(x => x.BookingId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetBookingByIdDto>(value);
    }
}