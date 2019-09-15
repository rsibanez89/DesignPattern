using Design.Patterns.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserQueryHandlers
	{
		public async Task<IEnumerable<UserState>> HandleAsync(IEnumerable<UserState> state, GetUsers msg)
		{
			return await repository.ListAsync();
		}
	}
}
