using Design.Patterns.Core;
using System;
using System.Collections.Generic;

namespace Design.Patterns.WebApi.Users
{
	public interface IUserQueryHandlers
		: IHandlerAsync<UserEntity, GetUser, MessageContext>,
		IHandlerAsync<IEnumerable<UserEntity>, GetUsers, MessageContext>,
		IHandlerAsync<Tuple<UserEntity, string>, AuthenticateUser, MessageContext>
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
