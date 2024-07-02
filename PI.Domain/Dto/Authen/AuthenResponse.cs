namespace PI.Domain.Dto.Authen
{
    public class AuthenResponse
    {
        public string AccessToken { get; set; } = null!;

        public DateTime ExpirationTime { get; set; }
    }
}