using AutoMapper;
using Project3Travelin.Dtos.CategoryDtos;
using Project3Travelin.Dtos.CommantDtos;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Entities;

namespace Project3Travelin.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Category, ResultCategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        CreateMap<Category, GetCategoryByIdDto>().ReverseMap();
        
        CreateMap<Tour, GetTourByIdDto>().ReverseMap();
        CreateMap<Tour, ResultTourDto>().ReverseMap();
        CreateMap<Tour, CreateTourDto>().ReverseMap();
        CreateMap<Tour, UpdateTourDto>().ReverseMap();
        
        CreateMap<Comment, GetCommentByIdDto>().ReverseMap();
        CreateMap<Comment, ResultCommentDto>().ReverseMap();
        CreateMap<Comment, CreateCommentDto>().ReverseMap();
        CreateMap<Comment, UpdateCommentDto>().ReverseMap();
        CreateMap<Comment, ResultCommentListByTourIdDto>().ReverseMap();
    }
}