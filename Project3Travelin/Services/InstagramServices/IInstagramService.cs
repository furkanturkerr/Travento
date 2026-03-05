using Project3Travelin.Dtos.InstagramDtos;

namespace Project3Travelin.Services.InstagramServices;

public interface IInstagramService
{
    Task<List<ResultInstagramDto>> GetAllInstagramsAsync();
    Task UpdateInstagramAsync(UpdateInstagramDto updateInstagramDto);
    Task CreateInstagramAsync(CreateInstagramDto createInstagramDto);
    Task DeleteInstagramAsync(string id);
    Task<GetInstagramByIdDto> GetInstagramByIdAsync(string id);
}