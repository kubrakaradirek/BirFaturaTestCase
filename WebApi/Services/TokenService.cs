using System.Text.Json.Nodes;

namespace WebApi.Services
{
    public class TokenService
    {
        //TokenService ==> Bu servis API ile iletişim kurarak access token alır. Alınan token, diğer API çağrılarında kullanılarak güvenli erişim sağlanır.Kimlik doğrulaması gerçekleştirilmiştir.

        //HTTP istemcisi oluşturmak için IHttpClientFactory bağımlılığı kullanılır.
        private readonly IHttpClientFactory _httpClientFactory;
        public TokenService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //API' den access token almak için tanımlanan metot
        public async Task<string> GetTokenAsync()
        {
            using var client = _httpClientFactory.CreateClient(); //HTTP istemcisi oluşturuldu.


            //Token almak için bir HTTP istemcisi oluşturuldu.
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://istest.birfatura.net/token"), //API'nin token endpointine istek yapıldı.
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent(new Dictionary<string, string>  //Form verisi olarak kullanıcı bilgileri gönderildi.
              {
                { "grant_type", "password"},  //OAuth2 için kimlik doğrulama türü
                { "username", "test@test.com"},
                { "password", "Test123."}
              }),
            };

            //HTTP isteğinin gönderilmesi ve yanıt alınması
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(); //Yanıt içeriği string alınır
                var tokenJson = JsonObject.Parse(content); //Json'a parse edilir

                //Json nesnesi null değilse ve access_token içeriyorsa, token değeri döndürür
                if (tokenJson != null && tokenJson["access_token"] != null) 
                {
                    return tokenJson["access_token"].ToString(); //Erişim token'ı döndürür
                }
            }
            return string.Empty;
        }
    }
}
