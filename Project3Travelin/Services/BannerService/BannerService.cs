using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.BannerDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.BannerService;

public class BannerService : IBannerService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Banner> _bannerCollection;
    
    public BannerService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _bannerCollection = database.GetCollection<Banner>(_databaseSettings.BannerCollectionName);
        _mapper = mapper;
    }

    public async Task<List<ResultBannerDto>> GetAllBannersAsync()
    {
        var values = await _bannerCollection.Find(x=> true).ToListAsync();
        return _mapper.Map<List<ResultBannerDto>>(values);
    }

    public async Task UpdateBannerAsync(UpdateBannerDto updateBannerDto)
    {
        var values = _mapper.Map<Banner>(updateBannerDto);
        await _bannerCollection.FindOneAndReplaceAsync(x => x.BannerId == updateBannerDto.BannerId, values);
    }

    public async Task CreateBannerAsync(CreateBannerDto createBannerDto)
    {
        var values = _mapper.Map<Banner>(createBannerDto);
        await _bannerCollection.InsertOneAsync(values);
    }

    public async Task DeleteBannerAsync(string id)
    {
        await _bannerCollection.DeleteOneAsync(x => x.BannerId == id);
    }

    public async Task<GetBannerByIdDto> GetBannerByIdAsync(string id)
    {
        var values = await _bannerCollection.Find(x => x.BannerId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetBannerByIdDto>(values);
    }
}