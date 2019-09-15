using Design.Patterns.Core;
using Newtonsoft.Json;
using System;

namespace Design.Patterns.WebApi.Users
{
	public class UserState : State
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}
}
