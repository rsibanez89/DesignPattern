using Design.Patterns.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Design.Patterns.WebApi.Users
{
	public class UserState : State
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public byte[] Password { get; set; }
		public byte[] Salt { get; set; }
		public string LastChangedPasswordOn { get; set; }
		public IEnumerable<UserRole> Roles { get; set; }
	}
}
