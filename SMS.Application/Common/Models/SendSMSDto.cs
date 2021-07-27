using Application.Common.Mappings;

namespace SMS.Application.Common.Models
{
    public class SendSMSDto
    {
        public string PhoneNumber { get; set; }
        public string SMSText { get; set; }
    }
}
