using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserCommandHandlers
	{
		public async Task<UserState> HandleAsync(AddUserRole msg, CancellationToken? cancellationToken)
		{
			var state = await repository.GetAsync(msg.Id);
			if(state.Roles.Contains(msg.Role))
			{
				return state;
			}
			state.Roles = state.Roles.Append(msg.Role);
			state.Version = msg.Version;

			return await repository.UpdateAsync(state);
		}
	}
}
