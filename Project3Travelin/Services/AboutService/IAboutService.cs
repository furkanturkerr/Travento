using Project3Travelin.Dtos.AboutDtos;

namespace Project3Travelin.Services.AboutService;

public interface IAboutService
{
    Task<List<ResultAboutDto>> GetAllAboutAsync();
    Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
    Task CreateAboutAsync(CreateAboutDto createAboutDto);
    Task DeleteAboutAsync(string id);
    Task<GetAboutByIdDto> GetAboutByIdAsync(string id);
}