using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.Application.Common.Models;
using SMS.Application.SMSFeature.Command.SendSMS;
using System.Threading.Tasks;

namespace API.Controllers
{
    [AllowAnonymous]
    public class SMSController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return "working..";
        }
        
    }
}