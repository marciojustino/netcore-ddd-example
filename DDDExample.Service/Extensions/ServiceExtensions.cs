namespace DDDExample.Service.Extensions
{
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBaseService<User>, BaseService<User>>();
            return services;
        }
    }
}