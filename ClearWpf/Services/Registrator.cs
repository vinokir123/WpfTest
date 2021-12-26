using ClearWpf.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ClearWpf.Services
{
    internal static class Registrator
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ICovidDataParser, CovidDataParser>();
            services.AddSingleton<IDataService, DataService>();
            return services;
        }
    }
}
