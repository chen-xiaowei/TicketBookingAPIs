using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog.Web.LayoutRenderers;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using TicketBookingAPIs.Config;
using TicketBookingAPIs.Models;

namespace TicketBookingAPIs.Services
{
    public class JwtService : IJwtService
    {
        SymmetricSecurityKey _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsJson.SecretKey));
        public AccessTokenObject CreateToken(UserRequest req)
        {
            var signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //var encryptionkey = Encoding.UTF8.GetBytes("1234567890qwerty");
            //var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = GetClaim();

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:7137",
                Audience = "https://localhost:7137",
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(0),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = signingCredentials,
                //EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var refreshDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:7137",
                Audience = "https://localhost:7137",
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(0),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signingCredentials,
                //EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, "Refresh") })
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var accessToken = tokenHandler.CreateJwtSecurityToken(descriptor);
            var refreshToken = tokenHandler.CreateJwtSecurityToken(refreshDescriptor);
           
            return new AccessTokenObject(tokenHandler.WriteToken(accessToken), tokenHandler.WriteToken(refreshToken), (int)(accessToken.ValidTo - DateTime.UtcNow).TotalSeconds);
        }

        public AccessTokenObject RefreshToken(RefreshTokenRequest req)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //验证Token格式
            bool isCan = tokenHandler.CanReadToken(req.AccessToken);
            if(!isCan)
                return null;


            var validateParameter = new TokenValidationParameters()//验证参数
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://localhost:7137",
                ValidAudience = "https://localhost:7137",
                IssuerSigningKey = _symmetricSecurityKey
            };

            //验证传入的过期的AccessToken   
            SecurityToken validatedToken = null;

            tokenHandler.ValidateToken(req.AccessToken, validateParameter, out validatedToken);

            var signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var refreshDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:7137",
                Audience = "https://localhost:7137",
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(0),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signingCredentials,
                //EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, "Refresh") })
            };

            var jwtToken = validatedToken as JwtSecurityToken;//转换一下
            var claims = jwtToken.Claims;

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:7137",
                Audience = "https://localhost:7137",
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(0),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = signingCredentials,
                //EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var accessToken = tokenHandler.CreateJwtSecurityToken(descriptor);
            var refreshToken = tokenHandler.CreateJwtSecurityToken(refreshDescriptor);

            return new AccessTokenObject(tokenHandler.WriteToken(accessToken), tokenHandler.WriteToken(refreshToken), (int)(accessToken.ValidTo - DateTime.UtcNow).TotalSeconds);
        }

        private IEnumerable<Claim> GetClaim()
        {
            var list = new List<Claim>();
            //list.Add(new Claim(ClaimTypes.Name, req.UserName));

            //Add role authorization
            list.Add(new Claim(ClaimTypes.Role, "Admin"));

            return list;
        }
    }
}
