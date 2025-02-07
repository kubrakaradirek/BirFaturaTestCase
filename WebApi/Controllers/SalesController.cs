using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApi.Dtos;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //SalesController ==> Belirli bir API'den satış verilerini almak için yazılmıştır. TokenServce kullanarak erişim tokenı alır. Alınan token ile kimlik doğrulaması yaparak satış verilerini çeker. 
    public class SalesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TokenService _tokenService;
        public SalesController(IHttpClientFactory httpClientFactory, TokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            //Tokenı almak için TokenService kullanıldı.
            string token = await _tokenService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token alınamadı.");
            }

            //HTTP istemcisi oluşturuldu.
            using var client = _httpClientFactory.CreateClient();

            //API'ye yetkilendirme
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            //Satış verilerini almak için HTTP isteği
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://istest.birfatura.net/api/test/SatislarGetir"),
                Method = HttpMethod.Post,
                Content = new StringContent("", Encoding.UTF8, "application/json") //İçeriği boş ama JSON formatında
            };

            //HTTP isteği
            var responseMessage = await client.SendAsync(request);

            //JSON formatında veri alınması
            if (responseMessage.IsSuccessStatusCode)
            {
                var salesData = await responseMessage.Content.ReadFromJsonAsync<List<SalesInvoiceDto>>();
                return Ok(salesData);
            }

            return BadRequest("Satış verileri alınamadı.");
        }
    }
}



