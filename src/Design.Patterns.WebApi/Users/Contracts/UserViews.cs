using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public class UserView
	{
		public long Id { get; set; }
		public int Version { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
	}

	public static class UserStateToUserView
	{
		public static UserView ToUserView(this UserState userState, string token = null)
		{
			return new UserView
			{
				Id = userState.Id,
				Version = userState.Version,
				Email = userState.Email,
				FirstName = userState.FirstName,
				LastName = userState.LastName,
				Token = token
			};
		}
	}
}
