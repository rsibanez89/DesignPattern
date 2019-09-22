using Design.Patterns.Core;
using System.Collections.Generic;

namespace Design.Patterns.WebApi.Users
{
	public interface IUserQueryHandlers
		: IHandlerAsync<UserState, GetUser>,
		IHandlerAsync<IEnumerable<UserState>, GetUsers>
	{ }

	public partial class UserQueryHandlers : IUserQueryHandlers
	{
		private readonly IRepository<UserState> repository;

		public UserQueryHandlers(IRepository<UserState> repository)
		{
			this.repository = repository;
		}
	}
}
