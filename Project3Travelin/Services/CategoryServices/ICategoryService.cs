using Project3Travelin.Dtos.CategoryDtos;

namespace Project3Travelin.Services.CategoryServices;

public interface ICategoryService
{
    Task<List<ResultCategoryDto>> GetAllCategoriesAsync();
    Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
    Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task DeleteCategoryAsync(string id);
    Task<GetCategoryByIdDto> GetCategoryByIdAsync(string id);
}