using Project3Travelin.Dtos.FaqDtos;

namespace Project3Travelin.Services.FaqServices;

public interface IFaqService
{
    Task<List<ResultFaqDto>> GetAllFaqsAsync();
    Task UpdateFaqAsync(UpdateFaqDto updateFaqDto);
    Task CreateFaqAsync(CreateFaqDto createFaqDto);
    Task DeleteFaqAsync(string id);
    Task<GetFaqByIdDto> GetFaqByIdAsync(string id);
}