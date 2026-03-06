using Project3Travelin.Dtos.CommantDtos;

namespace Project3Travelin.Services.CommantServices;

public interface ICommentService
{
    Task<List<ResultCommentDto>> GetAllCommantsAsync();
    Task UpdateCommantAsync(UpdateCommentDto updateCommentDto);
    Task CreateCommantAsync(CreateCommentDto createCommentDto);
    Task DeleteCommantAsync(string id);
    Task ApproveCommentAsync(string id);
    Task<GetCommentByIdDto> GetCommentByIdAsync(string id);
    Task<ReviewAverageDto> GetAverageByTourIdAsync(string id);
    Task<List<ResultCommentListByTourIdDto>> GetCommentByTourIdAsync(string id);
}