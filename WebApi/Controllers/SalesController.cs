using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json.Nodes;
using WebApi.Dtos;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TokenService _tokenService;

        public SalesController(IHttpClientFactory httpClientFactory,TokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> GetSales()
        {
            // 1️⃣ Token'ı Al
            string token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token alınamadı.");
            }

            using var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // 2️⃣ Satış Verilerini POST isteği ile al
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://istest.birfatura.net/api/test/SatislarGetir"),
                Method = HttpMethod.Post,
                Content = new StringContent("", Encoding.UTF8, "application/json") // Boş bir body gönderiyoruz
            };

            var responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                var salesData = await responseMessage.Content.ReadFromJsonAsync<List<SalesInvoiceDto>>();
                return Ok(salesData); // 📌 Gelen JSON verisini döndür
            }

            return BadRequest("Satış verileri alınamadı.");
        }
    }
}

