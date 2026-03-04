using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.AboutDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.AboutService;

public class AboutService : IAboutService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<About> _aboutCollection;
    
    public AboutService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _aboutCollection = database.GetCollection<About>(_databaseSettings.AboutCollectionName);
        _mapper = mapper;
    }

    public async Task<List<ResultAboutDto>> GetAllAboutAsync()
    {
        var values = await _aboutCollection.Find(x=> true).ToListAsync();
        return _mapper.Map<List<ResultAboutDto>>(values);
    }

    public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
    {
        var values = _mapper.Map<About>(updateAboutDto);
        await _aboutCollection.FindOneAndReplaceAsync(x=>x.AboutId == updateAboutDto.AboutId , values);
    }

    public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
    {
        var values = _mapper.Map<About>(createAboutDto);
        await _aboutCollection.InsertOneAsync(values);
    }

    public async Task DeleteAboutAsync(string id)
    {
        await _aboutCollection.DeleteOneAsync(x => x.AboutId == id);
    }

    public async Task<GetAboutByIdDto> GetAboutByIdAsync(string id)
    {
        var values = await _aboutCollection.Find(x => x.AboutId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetAboutByIdDto>(values);
    }
}