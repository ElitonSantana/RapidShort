using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RapidShort.Domain.Entities;
using RapidShort.Domain.Services;
using RapidShort.Domain.Services.Interfaces;
using RapidShort.Infra.Context;
using RapidShort.Infra.Repository;

namespace RapidShort.Application
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureServicesExtension(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));

            ConfigureServices(builder.Services);
            ConfigureRepository(builder.Services);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUrlService, UrlService>();
        }

        private static void ConfigureRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
