using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using SMS.Application.Common.Models;
using SMS.Application.SMSFeature.Command.SendSMS;
using System.Text.Json;
using System.Threading.Tasks;

namespace SMS.API.EventBusConsumer
{
    public class SmsSendConsumer : IConsumer<SMSSendEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SmsSendConsumer> _logger;

        public SmsSendConsumer(IMediator mediator, ILogger<SmsSendConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SMSSendEvent> context)
        {
            SendSMSDto model = new SendSMSDto
            {
                PhoneNumber = context.Message.PhoneNumber,
                SMSText = context.Message.SMSText
            };

            var result = await _mediator.Send(new SendSMSCommand(model));

            _logger.LogInformation(string.Format("SMSSendEvent consumed successfully. response => {0},{1}" ,model.PhoneNumber, JsonSerializer.Serialize(result).ToString()));
        }
    }
}
