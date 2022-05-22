using Wond.Params.Dtos;
using Wond.Params.Models;
using Wond.Params.Repositories;
using Wond.Params.Services;

namespace Wond.Params.Configuration;

public static class ParamsDIConfiguration {

    public static void ConfigureDIForParams(this IServiceCollection services) {

        services.AddScoped<ICrudRepository<Category>, CategoriesRepository>();
        services.AddScoped<ICrudService<CategoryDto>, CategoriesService>();

        services.AddScoped<ICrudRepository<Color>, ColorsRepository>();
        services.AddScoped<ICrudService<ColorDto>, ColorsService>();

        services.AddScoped<ICrudRepository<Material>, MaterialsRepository>();
        services.AddScoped<ICrudService<MaterialDto>, MaterialsService>();

        services.AddScoped<ICrudRepository<Size>, SizesRepository>();
        services.AddScoped<ICrudService<SizeDto>, SizesService>();

    }
}
