using Newtonsoft.Json;
using System.Text;

namespace LineMessaging.Controllers
{
    public class LineService
    {
        public static string ChannelSecret = "1a8ca533b31f051577d25e48c5530c11"; // Replace with your Channel Secret
        public static string ChannelAccessToken = "S7Oc2/0LapW+vwOujRiASrU87Ii0GoWwI2SrGDNrHVJC065eF/KrIIatBd8SzULUiL9XoAJHpyoB2fsoncM/6PhiXEfh7KglA21itVlqlJXGEss18AhEN0R538IlZqIEfYqzzJYxvnPFgAp23i8KlwdB04t89/1O/w1cDnyilFU="; // Replace with your Channel Access Token

        public static bool VerifySignature(string xLineSignature, string requestBody)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(ChannelSecret)))
            {
                var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(requestBody)));
                return hash == xLineSignature;
            }
        }

        public static async Task ReplyMessage(string replyToken, string message)
        {
            var url = "https://api.line.me/v2/bot/message/reply";
            var payload = new
            {
                replyToken = replyToken,
                messages = new[]
                {
                    new { type = "text", text = message }
                }
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ChannelAccessToken}");
                var jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                await client.PostAsync(url, content);
            }
        }
    }
}
