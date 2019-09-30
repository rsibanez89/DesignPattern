using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public interface IJwtBearerService
	{
		string GetToken(string id, string userName);
	}

	public class JwtBearerService : IJwtBearerService
	{
		private readonly SigningCredentials SigningCredentials;
		private readonly JwtSecurityTokenHandler TokenHandler;
		private readonly string Audience;
		private readonly string Issuer;

		public JwtBearerService(IOptions<UserModuleOptions> options)
		{
			var key = Encoding.UTF8.GetBytes(options.Value.JwtIssuerSigningKey);
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
			TokenHandler = new JwtSecurityTokenHandler();
			Audience = options.Value.JwtAudience;
			Issuer = options.Value.JwtIssuer;
		}

		public string GetToken(string id, string userName)
		{
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, id)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = SigningCredentials,
				Audience = Audience,
				Issuer = Issuer
			};
			var token = TokenHandler.CreateToken(tokenDescriptor);
			return TokenHandler.WriteToken(token);
		}
	}
}
