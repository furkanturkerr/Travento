using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Driver;
using Project3Travelin.Dtos.PopulerDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.PopulerService;

public class PopulerService : IPopulerService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Populer> _commantsCollection;

    public PopulerService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _commantsCollection = database.GetCollection<Populer>(_databaseSettings.PopulerCollectionName);
        _mapper = mapper;
    }
    
    public async Task<List<ResultPopulerDto>> GetAllPopulersAsync()
    {
        var values = await _commantsCollection.Find(x=> true).ToListAsync();
        return _mapper.Map<List<ResultPopulerDto>>(values);
    }

    public async Task UpdatePopulerAsync(UpdatePopulerDto updatePopulerDto)
    {
        var value = _mapper.Map<Populer>(updatePopulerDto);
        await _commantsCollection.FindOneAndReplaceAsync(x => x.PopulerId == updatePopulerDto.PopulerId, value);
    }

    public async Task CreatePopulerAsync(CreatePopulerDto createPopulerDto)
    {
        var value = _mapper.Map<Populer>(createPopulerDto);
        await _commantsCollection.InsertOneAsync(value);
    }

    public async Task DeletePopulerAsync(string id)
    {
        await _commantsCollection.DeleteOneAsync(x => x.PopulerId == id);
    }

    public async Task<GetPopulerByIdDto> GetPopulerByIdAsync(string id)
    {
        var values  = await _commantsCollection.Find(x => x.PopulerId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetPopulerByIdDto>(values);
    }
}