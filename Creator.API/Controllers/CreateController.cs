using Creator.API.Models;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Creator.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CreateController : ControllerBase
    {
        private readonly ILogger<CreateController> _logger;

        public IPublishEndpoint _publishEndpoint { get; }
        public CreateController(IPublishEndpoint publishEndpoint, ILogger<CreateController> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "working..";
        }

        [Route("sendsmsqueue")]
        [HttpPost]
       
        public async Task<IActionResult> SendSmsQueue([FromBody] SendSMSDto sms)
        {
            try
            {
                if (!ModelState.IsValid)           
                    return BadRequest(ModelState); 

                _logger.LogInformation("Create sms queue event for phone : " + sms.PhoneNumber);

                // send checkout event to rabbitmq
                var eventMessage = new SMSSendEvent
                {
                    PhoneNumber = sms.PhoneNumber,
                    SMSText = sms.SMSText
                };
                await _publishEndpoint.Publish(eventMessage);

                return Accepted();
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }
    }
}
