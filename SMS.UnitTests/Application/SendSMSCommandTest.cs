using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using SMS.Application.Common.Interfaces;
using SMS.Application.Common.Models;
using SMS.Application.SMSFeature.Command.SendSMS;
using System.Threading.Tasks;
using Xunit;

namespace SMS.UnitTests.Application
{
    public class SendSMSCommandTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IExternalSMSService> _externalSMSService;
        private readonly Mock<IPublishEndpoint> _publishEndpoint;
        public SendSMSCommandTest()
        {
            _externalSMSService = new Mock<IExternalSMSService>();
            _mediator = new Mock<IMediator>();
            _publishEndpoint = new Mock<IPublishEndpoint>();
        }

        [Fact]
        public async Task Handle_return_true_if_sms_sent()
        {
            var LoggerMock = new Mock<ILogger<SendSMSCommandHandler>>();

            var handler = new SendSMSCommandHandler(_externalSMSService.Object,LoggerMock.Object, _publishEndpoint.Object);
            var cltToken = new System.Threading.CancellationToken();

            SendSMSDto model = new SendSMSDto
            {
                PhoneNumber = "009100002012",
                SMSText = "test content"
            };

            var fakeSMSCommand = new SendSMSCommand(model);

            var result = await handler.Handle(fakeSMSCommand, cltToken);

            Assert.True(result.IsSuccess);

        }

        [Fact]
        public void Handle_throws_exception_when_no_phone_number()
        {
            var validator = new SendSMSCommandValidator();
            SendSMSDto model = new SendSMSDto
            {
                SMSText = "test"
            };

            var fakeSMSCommand = new SendSMSCommand(model);
            var result = validator.Validate(fakeSMSCommand);

            Assert.False(result.IsValid);

        }

        [Fact]
        public void Handle_throws_exception_when_no_sms_content()
        {
            var validator = new SendSMSCommandValidator();
            SendSMSDto model = new SendSMSDto
            {
                PhoneNumber = "009100002012"
            };

            var fakeSMSCommand = new SendSMSCommand(model);
            var result = validator.Validate(fakeSMSCommand);

            Assert.False(result.IsValid);

        }

        [Fact]
        public void Handle_throws_exception_when_invalid_phone_no()
        {
            var validator = new SendSMSCommandValidator();
            SendSMSDto model = new SendSMSDto
            {
                PhoneNumber = "test",
                SMSText = "test"
            };

            var fakeSMSCommand = new SendSMSCommand(model);
            var result = validator.Validate(fakeSMSCommand);

            Assert.False(result.IsValid);

        }
    }
}
