using System.Diagnostics.CodeAnalysis;
using Application;
using Application.Interfaces.Stripe;
using Application.Services.Stripe;
using Domain.Repositories;
using Domain.SeedWork.Notification;
using Infra.Data;
using Infra.Data.Repository;
using Infra.Utils.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC
{
    [ExcludeFromCodeCoverage]
    public static class NativeInjector
    {
        public static void AddLocalHttpClients(this IServiceCollection services, IConfiguration configuration) {}

        public static void AddLocalServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddScoped<INotification, Notification>();
            services.AddSingleton<IContainer, ServiceProviderProxy>();
            services.AddScoped<IStripeProductService, StripeProductService>();
            services.AddScoped<IStripePriceService, StripePriceService>();
            services.AddScoped<IStripePaymentService, StripePaymentService>();
            #endregion

            #region Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPriceRepository, PriceRepository>();
            #endregion
        }

        public static void AddLocalUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = Builders.BuildConnectionString(configuration);
            services.AddDbContext<Context>(options => options.UseLazyLoadingProxies().UseSqlServer(connString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddLocalHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = Builders.BuildConnectionString(configuration);
            var cacheConnString = configuration["App:Settings:Cache"]!;
            services.AddHealthChecks()
                .AddSqlServer(connString)
                .AddRedis(cacheConnString);
        }
    }
}
