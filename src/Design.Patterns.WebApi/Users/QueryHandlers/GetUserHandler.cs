using Design.Patterns.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserQueryHandlers
	{
		public async Task<UserEntity> HandleAsync(GetUser msg, MessageContext messageContext)
		{
			return await repository.GetAsync(msg.Id);
		}
	}
}
