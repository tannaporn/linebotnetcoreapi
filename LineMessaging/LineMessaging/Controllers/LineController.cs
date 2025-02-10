using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using LineMessaging.Model;

namespace LineMessaging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LineController : ControllerBase
    {
        private readonly string ChannelSecret = ""; // Replace with your Channel Secret
        private readonly string ChannelAccessToken = ""; // Replace with your Channel Access Token

      
        [HttpPost("webhook")]
        public async Task<IActionResult> Post([FromBody] LineWebhookEvents body)
        {
            // Log the incoming request (optional)
            var requestBody = JsonConvert.SerializeObject(body);
            Console.WriteLine($"Request Body: {requestBody}");

            // Verify the signature (optional but recommended)
            var xLineSignature = Request.Headers["x-line-signature"];
            if (!LineService.VerifySignature(xLineSignature, requestBody))
            {
                return BadRequest("Invalid Signature");
            }

            // Parse the events
            var events = JsonConvert.DeserializeObject<LineWebhookEvents>(requestBody);
            foreach (var lineEvent in events.Events)
            {
                if (lineEvent.Type == "message")
                {
                    // Handle messages
                    await LineService.ReplyMessage(lineEvent.ReplyToken, "Hello! This is a response from the webhook.");
                }
            }

            return Ok();
        }

      

    }
   
}

