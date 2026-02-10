using Project3Travelin.Dtos.CommantDtos;

namespace Project3Travelin.Services.CommantServices;

public interface ICommentService
{
    Task<List<ResultCommentDto>> GetAllCommantsAsync();
    Task UpdateCommantAsync(UpdateCommentDto updateCommentDto);
    Task CreateCommantAsync(CreateCommentDto createCommentDto);
    Task DeleteCommantAsync(string id);
    Task<GetCommentByIdDto> GetCommentByIdAsync(string id);
}