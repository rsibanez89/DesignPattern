using Design.Patterns.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserCommandHandlers
	{
		public async Task<UserState> HandleAsync(UserState state, CreateUser msg)
		{
			var now = DateTime.Now;
			state.Id = msg.Id;
			state.CreatedOn = now;
			state.CreatedBy = 0;
			state.LastModifiedOn = now;
			state.CreatedBy = 0;
			state.Version = 1;
			state.FirstName = msg.FirstName;
			state.LastName = msg.LastName;
			state.Email = msg.Email;

			return await repository.SaveChangesAsync(state);
		}
	}
}
