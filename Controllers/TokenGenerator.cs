using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Security.Claims;

namespace WebApiFlowerTwo.Controllers
{
    internal static class TokenGenerator
    {
        public static string GenerateTokenJwt(string username)
        {
            // appsetting for Token JWT
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"]; //WHO
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"]; 
            //var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"]; //TIME 

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey)); // cREATE THE KEY
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature); // ALGORITHM USED HSAS256

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }); 

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler(); 
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken, 
                subject: claimsIdentity, //PAYLOAD
                notBefore: DateTime.UtcNow, 
                //expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
        public static string GenerateTokenJwt(string username,string role)
        {
            // appsetting for Token JWT
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"]; //WHO
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            //var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"]; //TIME 

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey)); // cREATE THE KEY
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature); // ALGORITHM USED HSAS256

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username),
                                                                        new Claim(ClaimTypes.Role,role)});

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity, //PAYLOAD
                notBefore: DateTime.UtcNow,
                //expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}