using Project3Travelin.Dtos.TourTineraryDtos;

namespace Project3Travelin.Services.TourTineraryService;

public interface ITineraryService
{
    Task<List<ResultTineraryDto>> GetAllTineraryAsync();
    Task UpdateTineraryAsync(UpdateTineraryDto updateTineraryDto);
    Task CreateTineraryAsync(CreateTineraryDto createTineraryDto);
    Task DeleteTineraryAsync(string id);
    Task<GetTineraryByIdDto> GetTineraryByIdAsync(string id);
    Task<List<ResultTineraryListByTourIdDto>> GetTineraryByTourIdAsync(string id);
}