using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.InstagramDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.InstagramServices;

public class InstagramService : IInstagramService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Instagram> _InstagramCollection;
    
    public InstagramService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _InstagramCollection = database.GetCollection<Instagram>(_databaseSettings.InstagramCollectionName);
        _mapper = mapper;
    }

    public async Task<List<ResultInstagramDto>> GetAllInstagramsAsync()
    {
        var values = await _InstagramCollection.Find(x=> true).ToListAsync();
        return _mapper.Map<List<ResultInstagramDto>>(values);
    }

    public async Task UpdateInstagramAsync(UpdateInstagramDto updateInstagramDto)
    {
        var values = _mapper.Map<Instagram>(updateInstagramDto);
        await _InstagramCollection.FindOneAndReplaceAsync(x => x.InstagramId == updateInstagramDto.InstagramId, values);
    }

    public async Task CreateInstagramAsync(CreateInstagramDto createInstagramDto)
    {
        var values = _mapper.Map<Instagram>(createInstagramDto);
        await _InstagramCollection.InsertOneAsync(values);
    }

    public async Task DeleteInstagramAsync(string id)
    {
        await _InstagramCollection.DeleteOneAsync(x => x.InstagramId == id);
    }

    public async Task<GetInstagramByIdDto> GetInstagramByIdAsync(string id)
    {
        var values = await _InstagramCollection.Find(x => x.InstagramId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetInstagramByIdDto>(values);
    }
}