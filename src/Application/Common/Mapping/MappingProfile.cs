using AutoMapper;
using Nest.Application.Dtos.Auth;
using Nest.Application.Dtos.Categories;
using Nest.Domain.Entities;

namespace Nest.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();

        CreateMap<AppUser, RegisterDto>().ReverseMap();
        CreateMap<AppUser, LoginDto>().ReverseMap();
    }
}
