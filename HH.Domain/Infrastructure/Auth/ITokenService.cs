using HH.Domain.Dto.Authen;
using System.Security.Claims;

namespace HH.Domain.Infrastructure.Auth
{
    public interface ITokenService
    {
        string Encode(GenerateTokenRequest data);
        IEnumerable<Claim> Decode(string token);
        IEnumerable<Claim> DecodeWithoutExpiration(string token);
    }
}
