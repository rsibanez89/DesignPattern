using Design.Patterns.Core;

namespace Design.Patterns.WebApi.Users
{
	public interface IUserCommandHandlers
		: IHandlerAsync<UserState, CreateUser>
	{ }

	public partial class UserCommandHandlers : IUserCommandHandlers
	{
		private readonly IRepository<UserState> repository;

		public UserCommandHandlers(IRepository<UserState> repository)
		{
			this.repository = repository;
		}
	}
}
