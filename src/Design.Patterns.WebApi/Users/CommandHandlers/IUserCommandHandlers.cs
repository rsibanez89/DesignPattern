using Design.Patterns.Core;

namespace Design.Patterns.WebApi.Users
{
	public interface IUserCommandHandlers
		: IHandlerAsync<UserState, CreateUser>,
		IHandlerAsync<UserState, UpdateUserDetails>,
		IHandlerAsync<UserState, UpdateUserPassword>,
		IHandlerAsync<UserState, DeleteUser>
	{ }

	public partial class UserCommandHandlers : IUserCommandHandlers
	{
		private readonly IRepository<UserState> repository;
		private readonly IPasswordService passwordService;

		public UserCommandHandlers(IRepository<UserState> repository, IPasswordService passwordService)
		{
			this.repository = repository;
			this.passwordService = passwordService;
		}
	}
}
