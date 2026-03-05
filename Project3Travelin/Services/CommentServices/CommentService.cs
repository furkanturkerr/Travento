using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.CommantDtos;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.CommantServices;

public class CommentService : ICommentService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Comment> _commantsCollection;

    public CommentService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _commantsCollection = database.GetCollection<Comment>(_databaseSettings.CommentCollectionName);
        _mapper = mapper;
    }

    public async Task<List<ResultCommentDto>> GetAllCommantsAsync()
    {
        var values = await _commantsCollection.Find(x=> true).ToListAsync();
        return _mapper.Map<List<ResultCommentDto>>(values);
    }

    public async Task UpdateCommantAsync(UpdateCommentDto updateCommentDto)
    {
        var value = _mapper.Map<Comment>(updateCommentDto);
        await _commantsCollection.FindOneAndReplaceAsync(x => x.CommentId == updateCommentDto.CommantId, value);
    }

    public async Task CreateCommantAsync(CreateCommentDto createCommentDto)
    {
        var values = _mapper.Map<Comment>(createCommentDto);
        await _commantsCollection.InsertOneAsync(values);
    }

    public async Task DeleteCommantAsync(string id)
    {
       await _commantsCollection.DeleteOneAsync(x => x.CommentId == id);
    }

    public async Task<GetCommentByIdDto> GetCommentByIdAsync(string id)
    {
        var vaalue = await _commantsCollection.Find(x => x.CommentId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetCommentByIdDto>(vaalue);
    }

    public async Task<List<ResultCommentListByTourIdDto>> GetCommentByTourIdAsync(string id)
    {
        var value = await _commantsCollection.Find(x => x.TourId == id).ToListAsync();
        return _mapper.Map<List<ResultCommentListByTourIdDto>>(value);
    }
    
    public async Task<ReviewAverageDto> GetAverageByTourIdAsync(string tourId)
    {
        var reviews = await _commantsCollection
            .Find(x => x.TourId == tourId && x.IsStatus == true)
            .ToListAsync();

        if (!reviews.Any()) return new ReviewAverageDto();

        return new ReviewAverageDto
        {
            Cleanliness   = Math.Round(reviews.Average(x => x.Cleanliness), 1),
            Facilities    = Math.Round(reviews.Average(x => x.Facilities), 1),
            ValueForMoney = Math.Round(reviews.Average(x => x.ValueForMoney), 1),
            Service       = Math.Round(reviews.Average(x => x.Service), 1),
            Location      = Math.Round(reviews.Average(x => x.Location), 1),
            Overall       = Math.Round(reviews.Average(x =>
                (x.Cleanliness + x.Facilities + x.ValueForMoney + x.Service + x.Location) / 5.0), 1),
            TotalReviews  = reviews.Count
        };
    }
}