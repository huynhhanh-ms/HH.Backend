namespace HH.Domain.Dto.Authen
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = null!;

        public DateTime ExpirationTime { get; set; }
    }
}