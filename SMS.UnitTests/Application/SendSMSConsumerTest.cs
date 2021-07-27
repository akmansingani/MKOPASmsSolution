using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Testing;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using SMS.API.EventBusConsumer;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SMS.UnitTests.Application
{

    public class SendSMSConsumerTest
    {
        private readonly Mock<IMediator> _mediator;
        public SendSMSConsumerTest()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task Should_publish_consume_send_sms_event()
        {
            var LoggerMock = new Mock<ILogger<SmsSendConsumer>>();

            var harness = new InMemoryTestHarness();

            var consumerHarness = harness.Consumer(() => new SmsSendConsumer(_mediator.Object,LoggerMock.Object));

            await harness.Start();

            try
            {
                await harness.Bus.Publish(new SMSSendEvent
                {
                    PhoneNumber = "00912313012312",
                    SMSText = "test"
                });

                 Assert.True(await harness.Published.Any<SMSSendEvent>());

                 Assert.True(await consumerHarness.Consumed.Any<SMSSendEvent>());
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}
