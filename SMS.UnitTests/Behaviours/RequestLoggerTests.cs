using Application.Common.Behaviours;
using Microsoft.Extensions.Logging;
using Moq;
using SMS.Application.Common.Models;
using SMS.Application.SMSFeature.Command.SendSMS;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SMS.UnitTests.Behaviours
{
    public class RequestLoggerTests
    {
        private readonly Mock<ILogger<SendSMSCommand>> _logger;

        public RequestLoggerTests()
        {
            _logger = new Mock<ILogger<SendSMSCommand>>();

        }

        [Fact]
        public async Task Call_to_logging_behavior()
        {
            var requestLogger = new RequestLogger<SendSMSCommand>(_logger.Object);
            var SmsModel = new SendSMSDto { PhoneNumber = "09100000001", SMSText = "test" };
            await requestLogger.Process(new SendSMSCommand(SmsModel), new CancellationToken());
        }
    }
}
