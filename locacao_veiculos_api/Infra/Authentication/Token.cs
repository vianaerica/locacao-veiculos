using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
using Microsoft.IdentityModel.Tokens;
using locacao_veiculos_api.Domain.Entities;
using locacao_veiculos_api.Domain.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace locacao_veiculos_api.Infra.Authentication
{
    public class Token : IToken
    {
        public string GerarToken(Usuario user)
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"appsettings.json")));
				var key = Encoding.ASCII.GetBytes(jAppSettings["JwtToken"].ToString());
				var expirationTime = Convert.ToInt32(jAppSettings["ExpirationTime"]);
				var tokenDescriptor = new SecurityTokenDescriptor()
				{
					Subject = new ClaimsIdentity(new Claim[]{
						new Claim(ClaimTypes.Name, user.Login),
						new Claim(ClaimTypes.Role, user.TipoUsuario.ToString()),
					}),
					Expires = DateTime.UtcNow.AddHours(expirationTime),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				return tokenHandler.WriteToken(token);
			}
        
    }
}