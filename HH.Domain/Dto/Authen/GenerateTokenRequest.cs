using HH.Domain.Enums;

namespace HH.Domain.Dto.Authen
{
    public class GenerateTokenRequest
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public AccountRole Role { get; set; }
        public int ExpireHours { get; set; }

    }
}