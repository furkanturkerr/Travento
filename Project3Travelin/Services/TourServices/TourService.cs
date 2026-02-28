using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.TourServices;

public class TourService : ITourService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Tour> _tourCollection;

    public TourService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
        _mapper = mapper;
    }

    public async Task<List<ResultTourDto>> GetAllTourAsync()
    {
        var values = await _tourCollection.Find(x=> true).ToListAsync();
        return _mapper.Map<List<ResultTourDto>>(values);
    }

    public async Task CreateTourAsync(CreateTourDto createTourDto)
    {
        var values = _mapper.Map<Tour>(createTourDto);
        await _tourCollection.InsertOneAsync(values);
    }

    public async Task UpdateTourAsync(UpdateTourDto updateTourDto)
    {
       var values = _mapper.Map<Tour>(updateTourDto);
       await _tourCollection.FindOneAndReplaceAsync(x => x.TourId == updateTourDto.TourId, values);
    }

    public async Task DeleteTourAsync(string id)
    {
        await _tourCollection.DeleteOneAsync(x => x.TourId == id);
    }

    public async Task<GetTourByIdDto> GetTourByIdAsync(string id)
    {
        var value = await _tourCollection.Find(x => x.TourId == id).FirstOrDefaultAsync();
    
        if (value == null)
            return null;
    
        return _mapper.Map<GetTourByIdDto>(value);
    }
    
    public async Task<List<ResultTourDto>> GetFilteredToursAsync(string city, decimal? minPrice, decimal? maxPrice)
    {
        var builder = Builders<Tour>.Filter;
        // Filter builder oluştur - SQL'deki WHERE gibi düşün
        var filter = builder.Empty;
        // Boş filter = filtre yok = hepsini getir
        // SQL: WHERE 1=1 (her zaman true)

        if (!string.IsNullOrEmpty(city))
        {
            // Regex yerine direkt eşitlik kontrolü
            filter = filter & builder.Eq(x => x.City, city);
            // & = AND ile birleştir
            // Eq = Equal (eşittir)
            // SQL: WHERE City = 'Roma'
        }

        if (minPrice.HasValue)
            filter = filter & builder.Gte(x => x.Price, minPrice.Value);

        if (maxPrice.HasValue)
            filter = filter & builder.Lte(x => x.Price, maxPrice.Value);
        

        var tours = await _tourCollection.Find(filter).ToListAsync();
        // Oluşturulan filtreyi MongoDB'ye gönder
        // SQL: SELECT * FROM Tours WHERE City='Roma' AND Price>=100 AND Price<=500
        return _mapper.Map<List<ResultTourDto>>(tours);
    }
    
}