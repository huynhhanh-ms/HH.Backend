using PI.Domain.Dto.Authen;
using System.Security.Claims;

namespace PI.Domain.Infrastructure.Auth
{
    public interface ITokenService
    {
        string Encode(GenerateTokenRequest data);
        IEnumerable<Claim> Decode(string token);
        IEnumerable<Claim> DecodeWithoutExpiration(string token);
    }
}
