using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.FaqDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.FaqServices;

public class FaqService : IFaqService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Faq> _FaqCollection;
    
    public FaqService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _FaqCollection = database.GetCollection<Faq>(_databaseSettings.FaqCollectionName);
        _mapper = mapper;
    }

    public async Task<List<ResultFaqDto>> GetAllFaqsAsync()
    {
        var values = await _FaqCollection.Find(x=> true).ToListAsync();
        return _mapper.Map<List<ResultFaqDto>>(values);
    }

    public async Task UpdateFaqAsync(UpdateFaqDto updateFaqDto)
    {
        var values = _mapper.Map<Faq>(updateFaqDto);
        await _FaqCollection.FindOneAndReplaceAsync(x => x.FaqId == updateFaqDto.FaqId, values);
    }

    public async Task CreateFaqAsync(CreateFaqDto createFaqDto)
    {
        var values = _mapper.Map<Faq>(createFaqDto);
        await _FaqCollection.InsertOneAsync(values);
    }

    public async Task DeleteFaqAsync(string id)
    {
        await _FaqCollection.DeleteOneAsync(x => x.FaqId == id);
    }

    public async Task<GetFaqByIdDto> GetFaqByIdAsync(string id)
    {
        var values = await _FaqCollection.Find(x => x.FaqId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetFaqByIdDto>(values);
    }
}