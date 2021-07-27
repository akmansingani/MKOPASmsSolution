using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Creator.API.Models
{
    public class SendSMSDto
    {
        [Required, RegularExpression(@"^[0-9]+$")]
        public string PhoneNumber { get; set; }
        [Required]
        public string SMSText { get; set; }
    }
}
