using AutoMapper;
using Review.Domain.Models;
using ReviewsWebApplication.Models;

namespace ReviewsWebApplication;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Login, LoginViewModel>().ReverseMap();
    }
}