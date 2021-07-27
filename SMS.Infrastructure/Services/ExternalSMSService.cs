using Microsoft.Extensions.Logging;
using SMS.Application.Common.Interfaces;
using SMS.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Infrastructure.Services
{
    class ExternalSMSService : IExternalSMSService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalSMSService> _logger;

        public ExternalSMSService(HttpClient httpClient, ILogger<ExternalSMSService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> SendSMS(SendSMSDto sms)
        {
            _logger.LogInformation("Calling http sms service/api, phone number : " + sms.PhoneNumber);

            // _httpClient => call api

            return true;
        }
    }
}
