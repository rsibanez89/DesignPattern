using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Design.Patterns.WebApi.Users
{
	public interface IJwtBearerService
	{
		string GetToken(UserEntity user);
		UserEntity GetUserFromToken(string token);
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

		public string GetToken(UserEntity user)
		{
			var claims = user.Roles
				.Select(role => new Claim(ClaimTypes.Role, role.ToString()))
				.Append(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = SigningCredentials,
				Audience = Audience,
				Issuer = Issuer
			};
			var token = TokenHandler.CreateToken(tokenDescriptor);
			return TokenHandler.WriteToken(token);
		}

		public UserEntity GetUserFromToken(string token)
		{
			var securityToken = (JwtSecurityToken)TokenHandler.ReadToken(token);

			return new UserEntity
			{
				Id = securityToken.Claims.Where(c => c.Type == "nameid").Select(c => long.Parse(c.Value)).Single(),
				Roles = securityToken.Claims.Where(c => c.Type == "role").Select(c => Enum.Parse<UserRole>(c.Value))
			};
		}
	}
}
