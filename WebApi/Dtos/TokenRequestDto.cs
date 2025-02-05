namespace WebApi.Dtos
{
    public class TokenRequestDto
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
