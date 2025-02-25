using Contact.Domain.AggregatesModel.GroupAggregate.Contracts;
using Contact.Domain.AggregatesModel.GroupAggregate.Services;
using Contact.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utilities.Framework.Contracts;

namespace Contact.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories(configuration);
            return services;
        }
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ContactContext>();
                dbContext.Database.Migrate();
            }

            return app;
        }
        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContactContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
                options.EnableSensitiveDataLogging();
            });
        }
        private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IWriteRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupNumberRepository, GroupNumberRepository>();
            services.AddScoped<IGroupSettingsRepository, GroupSettingsRepository>();
            services.AddTransient<IGroupValidatorService, GroupValidatorService>();
            services.AddTransient<IGroupNumberValidatorService, GroupNumberValidatorService>();

        }
    }
}
