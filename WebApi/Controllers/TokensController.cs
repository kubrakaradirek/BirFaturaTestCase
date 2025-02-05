using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Nodes;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TokensController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("get-token")]
        public async Task<IActionResult> GetToken()
        {
            string token = "";

            using var httpClient = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://istest.birfatura.net/token"),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", "password"},
                    { "username", "test@test.com"},
                    { "password", "Test123."}
                }),
            };

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonObject.Parse(content);

                if (tokenResponse != null && tokenResponse["access_token"] != null)
                {
                    token = tokenResponse["access_token"].ToString();
                    return Ok(new { token });
                }

                return BadRequest("Token response içinde access_token bulunamadı.");
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            return BadRequest($"Token alınırken bir hata oluştu. Hata: {errorResponse}");
        }

    }
}

