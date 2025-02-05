using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;
using WebUI.Dtos;

namespace WebUI.Controllers
{
    public class TokenController : Controller
    {
       private readonly IHttpClientFactory _httpClientFactory;

        public TokenController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken()
        {
            var tokenRequest = new TokenRequestDto
            {
                Userrname = "test@test.com",
                Password = "Test123."
            };

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(tokenRequest);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("http://istest.birfatura.net/api/auth/token", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                
                var token = ExtractTokenFromJson(jsonResponse);

                
                return Ok(token);
            }

            return BadRequest("Token alırken bir hata oluştu.");
        }

        private string ExtractTokenFromJson(string json)
        {
            var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(json);
            return jsonObject["access_token"].ToString(); // Token'ı alıyoruz
        }
    }
}
