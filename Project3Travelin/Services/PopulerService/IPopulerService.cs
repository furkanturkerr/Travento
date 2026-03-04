using Project3Travelin.Dtos.PopulerDtos;

namespace Project3Travelin.Services.PopulerService;

public interface IPopulerService
{
    Task<List<ResultPopulerDto>> GetAllPopulersAsync();
    Task UpdatePopulerAsync(UpdatePopulerDto updatePopulerDto);
    Task CreatePopulerAsync(CreatePopulerDto createPopulerDto);
    Task DeletePopulerAsync(string id);
    Task<GetPopulerByIdDto> GetPopulerByIdAsync(string id);
}