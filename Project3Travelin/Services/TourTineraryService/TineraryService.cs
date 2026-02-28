using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.TourTineraryDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.TourTineraryService;

public class TineraryService : ITineraryService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<TourItinerary> _itineraryCollection;

    public TineraryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _itineraryCollection = database.GetCollection<TourItinerary>(
            databaseSettings.TourItineraryCollectionName);
        _mapper = mapper;
    }

    public async Task<List<ResultTineraryDto>> GetAllTineraryAsync()
    {
        var values = await _itineraryCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultTineraryDto>>(values);
    }

    public async Task CreateTineraryAsync(CreateTineraryDto createTineraryDto)
    {
        var values = _mapper.Map<TourItinerary>(createTineraryDto); // Tour değil TourItinerary
        await _itineraryCollection.InsertOneAsync(values);
    }

    public async Task DeleteTineraryAsync(string id)
    {
        await _itineraryCollection.DeleteOneAsync(x => x.ItineraryId == id);
    }

    public async Task<GetTineraryByIdDto> GetTineraryByIdAsync(string id)
    {
        var values = await _itineraryCollection.Find(x => x.ItineraryId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetTineraryByIdDto>(values);
    }

    public async Task<List<ResultTineraryListByTourIdDto>> GetTineraryByTourIdAsync(string tourId)
    {
        var values = await _itineraryCollection.Find(x => x.TourId == tourId).ToListAsync();
        return _mapper.Map<List<ResultTineraryListByTourIdDto>>(values);
    }

    public async Task UpdateTineraryAsync(UpdateTineraryDto updateTineraryDto)
    {
        var values = _mapper.Map<TourItinerary>(updateTineraryDto);
        await _itineraryCollection.FindOneAndReplaceAsync(
            x => x.ItineraryId == updateTineraryDto.ItineraryId, values);
    }
}