using Design.Patterns.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public class UserView : State
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public IEnumerable<UserRole> Roles { get; set; }
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
				CreatedBy = userState.CreatedBy,
				CreatedOn = userState.CreatedOn,
				LastModifiedBy = userState.LastModifiedBy,
				LastModifiedOn = userState.LastModifiedOn,
				DeletedBy = userState.DeletedBy,
				DeletedOn = userState.DeletedOn,
				Email = userState.Email,
				FirstName = userState.FirstName,
				LastName = userState.LastName,
				Roles = userState.Roles,
				Token = token
			};
		}
	}
}
