namespace DDDExample.Infra.Data.Extensions
{
    using Context;
    using DDDExample.Domain.Configs;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Repository;

    public static class InfraExtensions
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfig = new DatabaseConfigurations();
            configuration.Bind("Database", databaseConfig);
            services.AddSingleton(databaseConfig);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(databaseConfig.ConnectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
            return services;
        }
    }
}