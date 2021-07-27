using Application.Common.Models;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using SMS.Application.Common.Interfaces;
using SMS.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SMS.Application.SMSFeature.Command.SendSMS
{
    public class SendSMSCommand : IRequest<Result<bool>>
    {
        public SendSMSDto SmsModel { get; set; }

        public SendSMSCommand(SendSMSDto smsModel)
        {
            SmsModel = smsModel;
        }
    }

    public class SendSMSCommandHandler : IRequestHandler<SendSMSCommand, Result<bool>>
    {
        private readonly ILogger<SendSMSCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public IExternalSMSService _externalSMSService { get; }

        public SendSMSCommandHandler(IExternalSMSService externalSMSService, ILogger<SendSMSCommandHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _externalSMSService = externalSMSService;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }


        public async Task<Result<bool>> Handle(SendSMSCommand request, CancellationToken cancellationToken)
        {
            // call external sms api
            var apiResult = await _externalSMSService.SendSMS(request.SmsModel);

            _logger.LogInformation("Create integration even after sms completion, phone number : " + request.SmsModel.PhoneNumber);

            // create sms integration event
            SMSSendCompletedEvent eventMessage = new SMSSendCompletedEvent
            {
                PhoneNumber = request.SmsModel.PhoneNumber,
                SMSText = request.SmsModel.SMSText
            };
            await _publishEndpoint.Publish(eventMessage);

            return Result<bool>.success(apiResult);
        }
    }
}
