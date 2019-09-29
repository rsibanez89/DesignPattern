using Design.Patterns.Core;
using System;
using System.Collections.Generic;

namespace Design.Patterns.WebApi.Users
{
	public interface IUserQueryHandlers
		: IHandlerAsync<UserState, GetUser>,
		IHandlerAsync<IEnumerable<UserState>, GetUsers>,
		IHandlerAsync<Tuple<UserState, string>, AuthenticateUser>
	{ }

	public partial class UserQueryHandlers : IUserQueryHandlers
	{
		private readonly IUserRepository repository;
		private readonly IPasswordService passwordService;
		private readonly IJwtBearerService jwtBearerService;

		public UserQueryHandlers(IUserRepository repository, IPasswordService passwordService, IJwtBearerService jwtBearerService)
		{
			this.repository = repository;
			this.passwordService = passwordService;
			this.jwtBearerService = jwtBearerService;
		}
	}
}
