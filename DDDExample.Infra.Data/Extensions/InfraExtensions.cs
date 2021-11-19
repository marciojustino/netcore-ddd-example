namespace DDDExample.Infra.Data.Extensions
{
    using Context;
    using Domain.Configs;
    using Domain.Entities;
    using Domain.Interfaces;
    using Domain.Interfaces.Auth;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Repository;

    public static class InfraExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfig = new DatabaseConfigurations();
            configuration.GetSection("Database").Bind(databaseConfig);
            services.AddSingleton(databaseConfig);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(databaseConfig.ConnectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            return services;
        }
    }
}