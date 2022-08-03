using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ARTJ.Apresentacao.Service
{
    public static class TokenService
    {
        public static String GenerateToken(Model.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); //Instanciando JWT
            var key = Encoding.ASCII.GetBytes(Settings.SecretKey);//Encodando a minha chave
            var tokenDescripto = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor() //Descrever tudo que o tokem tem 
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),//User.Idetity.Nome(Basicamente verificar spnet)
                    new Claim(ClaimTypes.Role, user.Role.ToString()),//
                    new Claim(ClaimTypes.UserData, user.data.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8), //Passou dessa hora tem que fazer um processo de refresh token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //Credencias usadas para cirptar e desincliptar o token 

            };

            var token = tokenHandler.CreateToken(tokenDescripto);
            return tokenHandler.WriteToken(token);

        }
    }
}
