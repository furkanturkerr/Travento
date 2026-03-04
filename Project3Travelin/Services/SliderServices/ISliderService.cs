using Project3Travelin.Dtos.SliderDtos;

namespace Project3Travelin.Services.SliderServices;

public interface ISliderService
{
    Task<List<ResultSliderDto>> GetAllSlidersAsync();
    Task UpdateSliderAsync(UpdateSliderDto updateSliderDto);
    Task CreateSliderAsync(CreateSliderDto createSliderDto);
    Task DeleteSliderAsync(string id);
    Task<GetSliderByIdDto> GetSliderByIdAsync(string id);
}