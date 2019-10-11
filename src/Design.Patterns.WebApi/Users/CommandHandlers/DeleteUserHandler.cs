using Design.Patterns.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserCommandHandlers
	{
		public async Task<UserEntity> HandleAsync(DeleteUser msg, MessageContext messageContext)
		{
			var state = await repository.GetAsync(msg.Id);
			state.Version = msg.Version;

			return await repository.DeleteAsync(state);
		}
	}
}
