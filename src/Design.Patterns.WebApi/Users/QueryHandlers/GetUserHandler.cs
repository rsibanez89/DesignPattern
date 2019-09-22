using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserQueryHandlers
	{
		public async Task<UserState> HandleAsync(GetUser msg, CancellationToken? cancellationToken)
		{
			return await repository.GetAsync(msg.Id);
		}
	}
}
