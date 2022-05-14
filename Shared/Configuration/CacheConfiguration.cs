using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wond.Shared.Configuration;

public static class CacheConfiguration {

    public static void ConfigureDistributedCache(this IServiceCollection services, ConfigurationManager configuration){
        services.AddStackExchangeRedisCache(options => options.Configuration = $"{configuration["Redis:Host"]}:{configuration["Redis:port"]}");
    }

}
