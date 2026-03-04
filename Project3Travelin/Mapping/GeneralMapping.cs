using AutoMapper;
using Project3Travelin.Dtos.AboutDtos;
using Project3Travelin.Dtos.CategoryDtos;
using Project3Travelin.Dtos.CommantDtos;
using Project3Travelin.Dtos.PopulerDtos;
using Project3Travelin.Dtos.SliderDtos;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Dtos.TourTineraryDtos;
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
        
        CreateMap<TourItinerary, ResultTineraryListByTourIdDto>().ReverseMap();
        CreateMap<TourItinerary, ResultTineraryDto>().ReverseMap();
        CreateMap<TourItinerary, GetTineraryByIdDto>().ReverseMap();
        CreateMap<TourItinerary, CreateTineraryDto>().ReverseMap();
        CreateMap<TourItinerary, UpdateTineraryDto>().ReverseMap();
        
        CreateMap<Slider, GetSliderByIdDto>().ReverseMap();
        CreateMap<Slider, ResultSliderDto>().ReverseMap();
        CreateMap<Slider, CreateSliderDto>().ReverseMap();
        CreateMap<Slider, UpdateSliderDto>().ReverseMap();
        CreateMap<UpdateSliderDto, GetSliderByIdDto>().ReverseMap();
        
        CreateMap<Populer, ResultPopulerDto>().ReverseMap();
        CreateMap<Populer, GetPopulerByIdDto>().ReverseMap();
        CreateMap<Populer, CreatePopulerDto>().ReverseMap();
        CreateMap<Populer, UpdatePopulerDto>().ReverseMap();
        CreateMap<UpdatePopulerDto, GetPopulerByIdDto>().ReverseMap();
        
        CreateMap<About, GetAboutByIdDto>().ReverseMap();
        CreateMap<About, ResultAboutDto>().ReverseMap();
        CreateMap<About, CreateAboutDto>().ReverseMap();
        CreateMap<About, UpdateAboutDto>().ReverseMap();
        CreateMap<UpdateAboutDto, GetAboutByIdDto>().ReverseMap();
    }
}