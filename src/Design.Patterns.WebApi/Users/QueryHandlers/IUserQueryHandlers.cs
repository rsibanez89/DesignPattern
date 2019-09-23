using Design.Patterns.Core;
using System.Collections.Generic;

namespace Design.Patterns.WebApi.Users
{
	public interface IUserQueryHandlers
		: IHandlerAsync<UserState, GetUser>,
		IHandlerAsync<IEnumerable<UserState>, GetUsers>,
		IHandlerAsync<UserState, AuthenticateUser>
	{ }

	public partial class UserQueryHandlers : IUserQueryHandlers
	{
		private readonly IUserRepository repository;
		private readonly IPasswordService passwordService;

		public UserQueryHandlers(IUserRepository repository, IPasswordService passwordService)
		{
			this.repository = repository;
			this.passwordService = passwordService;
		}
	}
}
