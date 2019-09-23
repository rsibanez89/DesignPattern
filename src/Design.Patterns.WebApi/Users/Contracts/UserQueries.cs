using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public class GetUser
	{
		public long Id { get; set; }
	}

	public class GetUsers
	{
	}

	public class AuthenticateUser
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
