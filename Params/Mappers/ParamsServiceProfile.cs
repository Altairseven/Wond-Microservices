using AutoMapper;
using Wond.Params.Dtos;
using Wond.Params.Models;

namespace Wond.Params.Mappers;


public class ParamsServiceProfile : Profile {

    public ParamsServiceProfile() {

        CreateTwoWay<Category, CategoryDto>();
        CreateTwoWay<Color, ColorDto>();
        CreateTwoWay<Material, MaterialDto>();
        CreateTwoWay<Size, SizeDto>();

    }

    public void CreateTwoWay<T1, T2>() {
        CreateMap<T1, T2>();
        CreateMap<T2, T1>();
    }
}

