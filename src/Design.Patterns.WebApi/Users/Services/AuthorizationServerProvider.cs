using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			
			return Task.FromResult<object>(null);
		}

		public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{


			return base.GrantResourceOwnerCredentials(context);
		}
	}
}