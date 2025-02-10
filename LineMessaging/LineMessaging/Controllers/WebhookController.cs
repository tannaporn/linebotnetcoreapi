using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft;
using Newtonsoft.Json.Linq;

namespace LineMessaging.Controllers
{
    public class WebhookController : ControllerBase
    {
        private const string WHATSAPP_WEBHOOK_VERIFY_TOKEN = "my_verify_token";

        [HttpGet("webhook")]
        public IActionResult VerifyWebhook(
            [FromQuery(Name = "hub.mode")] string hubMode,
            [FromQuery(Name = "hub.challenge")] string hubChallenge,
            [FromQuery(Name = "hub.verify_token")] string hubVerifyToken)
        {
            // Verify the webhook configuration
            if (hubMode == "subscribe" && hubVerifyToken == WHATSAPP_WEBHOOK_VERIFY_TOKEN)
            {
               
                return Ok(hubChallenge);
            }

    
            return Unauthorized();
        }


        private bool VerifySignature(string payload, string signatureHeader)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(WHATSAPP_WEBHOOK_VERIFY_TOKEN)))
            {
                var computedSignature = Convert.ToBase64String(
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(payload))
                );

                return computedSignature == signatureHeader;
            }
        }



        [HttpPost]
        public IActionResult ReceiveMessage([FromBody] JObject request)
        {
            try
            {

                var messages = request["entry"]?[0]?["changes"]?[0]?["value"]?["messages"];
                if (messages != null)
                {
                    foreach (var message in messages)
                    {
                        var from = message["from"]?.ToString();
                        var text = message["text"]?["body"]?.ToString(); 

                     
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing webhook: {ex.Message}");
                return StatusCode(500, "Error processing webhook");
            }
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] JObject request)
        {
            try
            {

                var messages = request["entry"]?[0]?["changes"]?[0]?["value"]?["messages"];
                if (messages != null)
                {
                    foreach (var message in messages)
                    {
                        var from = message["from"]?.ToString();
                        var text = message["text"]?["body"]?.ToString();


                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing webhook: {ex.Message}");
                return StatusCode(500, "Error processing webhook");
            }
        }

    }
}
