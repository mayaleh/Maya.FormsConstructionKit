using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Maya.FormsConstructionKit.Api.CQRS
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            return services;
        }
    }
}