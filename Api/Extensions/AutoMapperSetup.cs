using System;
using Core.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperConfig(this IServiceCollection service){

            if(service == null) throw new ArgumentNullException(nameof(service));

            service.AddAutoMapper(typeof(Startup));
            service.AddSingleton(MappingsConfig.Register().CreateMapper());

        }    
    
    }
}