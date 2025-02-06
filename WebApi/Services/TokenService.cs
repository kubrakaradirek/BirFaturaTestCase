using Dto;
using System.Text.Json.Nodes;

namespace WebApi.Services
{
    public class TokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TokenService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetTokenAsync()
        {
            using var client = _httpClientFactory.CreateClient();

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

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tokenJson = JsonObject.Parse(content);
                if (tokenJson != null && tokenJson["access_token"] != null)
                {
                    return tokenJson["access_token"].ToString();
                }
            }
            return string.Empty;
        }
    }
}
