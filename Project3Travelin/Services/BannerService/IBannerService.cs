using Project3Travelin.Dtos.BannerDtos;

namespace Project3Travelin.Services.BannerService;

public interface IBannerService
{
    Task<List<ResultBannerDto>> GetAllBannersAsync();
    Task UpdateBannerAsync(UpdateBannerDto updateBannerDto);
    Task CreateBannerAsync(CreateBannerDto createBannerDto);
    Task DeleteBannerAsync(string id);
    Task<GetBannerByIdDto> GetBannerByIdAsync(string id);
}