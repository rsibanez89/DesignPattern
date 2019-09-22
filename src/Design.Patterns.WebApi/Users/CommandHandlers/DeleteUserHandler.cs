using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserCommandHandlers
	{
		public async Task<UserState> HandleAsync(DeleteUser msg, CancellationToken? cancellationToken)
		{
			var state = await repository.GetAsync(msg.Id);
			state.Version = msg.Version;

			return await repository.DeleteAsync(state);
		}
	}
}
