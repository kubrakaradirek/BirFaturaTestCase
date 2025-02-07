using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    //SalesController==> Satış verilerini bir Web API'den çekerek Viewkatmanında listelemek için kullanıldı.Web API'den satış verilerini alarak JSON formatındaki veriyi SalesInvoiceViewModel nesnelerine dönüştürdüm. Alınan veriyi bir viewe gnderek kullanıcılara satış faturaları gösterildi. 
    public class SalesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SalesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async  Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7044/api/Sales");//Localdeki API'ye get isteği göndererek satış verilerinin çekilmesi
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<SalesInvoiceViewModel>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
