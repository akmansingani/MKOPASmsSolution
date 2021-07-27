using SMS.Application.Common.Models;
using System.Threading.Tasks;

namespace SMS.Application.Common.Interfaces
{
    public interface IExternalSMSService
    {
        Task<bool> SendSMS(SendSMSDto sms);
    }
}
