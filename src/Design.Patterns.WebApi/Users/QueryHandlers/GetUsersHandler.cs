using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserQueryHandlers
	{
		public async Task<IEnumerable<UserState>> HandleAsync(GetUsers msg, CancellationToken? cancellationToken)
		{
			return await repository.ListAsync();
		}
	}
}
