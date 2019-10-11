using Design.Patterns.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserQueryHandlers
	{
		public async Task<IEnumerable<UserEntity>> HandleAsync(GetUsers msg, MessageContext messageContext)
		{
			return await repository.ListAsync();
		}
	}
}
