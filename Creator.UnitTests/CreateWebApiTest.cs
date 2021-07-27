using Creator.API.Controllers;
using Creator.API.Models;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Creator.UnitTests
{
    public class CreateWebApiTest
    {
        private readonly Mock<ILogger<CreateController>> _loggerMock;
        private readonly Mock<IPublishEndpoint> _publishEndpoint;
        public CreateWebApiTest()
        {
            _loggerMock = new Mock<ILogger<CreateController>>();
            _publishEndpoint = new Mock<IPublishEndpoint>();
        }

        [Fact]
        public async Task Handle_post_send_sms_to_queue()
        {
            var createController = new CreateController(_publishEndpoint.Object, _loggerMock.Object);

            SendSMSDto model = new SendSMSDto
            {
                PhoneNumber = "009100002012",
                SMSText = "test content"
            };

            var actionResult = await createController.SendSmsQueue(model);

            Assert.Equal((actionResult as AcceptedResult).StatusCode, (int)System.Net.HttpStatusCode.Accepted);
        }

        [Fact]
        public async Task Handle_post_send_sms_to_queue_integration_event()
        {
            var createController = new CreateController(_publishEndpoint.Object, _loggerMock.Object);

            SendSMSDto model = new SendSMSDto
            {
                PhoneNumber = "009100002012",
                SMSText = "test content"
            };

            var result = await createController.SendSmsQueue(model) as AcceptedResult;

            _publishEndpoint.Verify(mock => mock.Publish(It.IsAny<SMSSendEvent>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(result);
        }
    }
}
