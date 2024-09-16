using Microsoft.IdentityModel.Tokens;
using HH.Domain.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HH.Domain.Dto.Authen;
using HH.Domain.Infrastructure.Auth;
using HH.Domain.Common;

namespace HH.Infrastructure.Auth
{
    public class JWTTokenService : ITokenService
    {
        private readonly SigningCredentials _credentials;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JWTTokenService()
        {
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JwtSetting.IssuerSigningKey));
            _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public string Encode(GenerateTokenRequest data)
        {
            var claimIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, data.Id),
                new Claim(ClaimTypes.Name, data.Username),
                new Claim("fullname", data.Fullname),
                new Claim(ClaimTypes.Role, data.Role.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = AppConfig.JwtSetting.ValidIssuer,
                Audience = AppConfig.JwtSetting.ValidAudience,
                Expires = DateTime.UtcNow.AddHours(data.ExpireHours),
                SigningCredentials = _credentials,
                Subject = claimIdentity,
                IssuedAt = DateTime.UtcNow
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<Claim> Decode(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims;
            }
            catch (SecurityTokenExpiredException)
            {
                throw new UnauthorizedException("Token is expired");
            }
            catch (Exception ex)
            {
                throw new UnauthorizedException("Token is invalid");
            }
        }

        //decode without expiration check
        public IEnumerable<Claim> DecodeWithoutExpiration(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedException("Token is invalid");
            }
        }
    }
}
