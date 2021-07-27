using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using SMS.Application.Common.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SMS.Infrastructure.Services
{
    public static class ExternalSMSClientConfiguration
    {
        public static IServiceCollection AddExternalSMSClient(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<ExternalSMSClientApiHandler>();
            services.AddHttpClient<IExternalSMSService, ExternalSMSService>()
                .ConfigureHttpClient(client =>
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .AddHttpMessageHandler<ExternalSMSClientApiHandler>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))  
                .AddPolicyHandler(GetRetryPolicy()); ;
            return services;
        }

        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,retryAttempt)));
        }

    }

    public class ExternalSMSClientApiHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, cancellationToken);
        }

    }
}
