using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Libs
{
    public static class Util
    {
        public readonly static string JWT_KEY_STR = "/3nS5pjNcbz8vx9fCKRX+a+9kBB+cLOLfsDysPesW8lQB7hBxBars7SBxiAxSKk=";
        public static string GenerateJwtToken(UserModel user)
        {

            byte[] secret = Encoding.ASCII.GetBytes(Util.JWT_KEY_STR);
            var securityKey = new SymmetricSecurityKey(secret);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = "Fiap",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
