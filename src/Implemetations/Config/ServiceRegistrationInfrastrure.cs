using Application.Interface.Services;
using Implementation.Services;
using Microsoft.Extensions.DependencyInjection;
using SmartApartment.Application.Interface.Services;


namespace Implemetation.Config
{
 
        public static class ServiceRegistrationInfrastrure
        {

            public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
            {

            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IUploadServices, UploadServices>();

            return services;
            }
        }
    }

