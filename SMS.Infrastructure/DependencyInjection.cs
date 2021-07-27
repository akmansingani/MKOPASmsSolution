using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SMS.Infrastructure.Services;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExternalSMSClient(configuration);
            return services;
        }


        public static ILoggerFactory AddInfrastructureConfigure(this ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            loggerFactory.AddApplicationInsights(configuration.GetSection("AppSettings:ApplicationInsightsKey").Value);

            return loggerFactory;
        }
    }
            
          
}
