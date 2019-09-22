using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserCommandHandlers
	{
		public async Task<UserState> HandleAsync(UpdateUserDetails msg, CancellationToken? cancellationToken)
		{
			var state = await repository.GetAsync(msg.Id);
			state.FirstName = msg.FirstName;
			state.LastName = msg.LastName;
			state.Email = msg.Email;
			state.Version = msg.Version;

			return await repository.UpdateAsync(state);
		}
	}
}
