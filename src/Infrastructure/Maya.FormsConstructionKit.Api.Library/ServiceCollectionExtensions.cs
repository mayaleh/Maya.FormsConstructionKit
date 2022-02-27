using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Maya.FormsConstructionKit.Api.Library
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppLibraryServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mappers.Storage.SharedModelProfile), typeof(Mappers.Shared.Model.StorageModelProfile));
            return services;
        }
    }
}