using Creator.API.Models;
using Creator.FunctionalTests.Base;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Creator.FunctionalTests
{
    public class CreatorScenarios
      : CreatorScenarioBase
    {
        [Fact]
        public async Task Post_send_sms_queue_and_response_ok_status_code()
        {
            using (var server = CreateServer())
            {
                var content = new StringContent(BuildSMS(), UTF8Encoding.UTF8, "application/json");
                  var response = await server.CreateClient()
                     .PostAsync(Post.SMSSendQueue, content);

                response.EnsureSuccessStatusCode();
            }
        }

        string BuildSMS()
        {
            SendSMSDto sms = new SendSMSDto
            {
                PhoneNumber = "009100002012",
                SMSText = "test content"
            };

            return JsonSerializer.Serialize(sms);
        }
    }
}
