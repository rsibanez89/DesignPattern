using Design.Patterns.Core;

namespace Design.Patterns.WebApi.Users
{
	public interface IUserCommandHandlers
		: IHandlerAsync<UserEntity, CreateUser, MessageContext>,
		IHandlerAsync<UserEntity, UpdateUserDetails, MessageContext>,
		IHandlerAsync<UserEntity, UpdateUserPassword, MessageContext>,
		IHandlerAsync<UserEntity, AddUserRole, MessageContext>,
		IHandlerAsync<UserEntity, DeleteUser, MessageContext>
	{ }

	public partial class UserCommandHandlers : IUserCommandHandlers
	{
		private readonly IUserRepository repository;
		private readonly IPasswordService passwordService;

		public UserCommandHandlers(IUserRepository repository, IPasswordService passwordService)
		{
			this.repository = repository;
			this.passwordService = passwordService;
		}
	}
}
