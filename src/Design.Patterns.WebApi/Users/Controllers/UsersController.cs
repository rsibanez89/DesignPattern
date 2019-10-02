using Design.Patterns.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserCommandHandlers userCommandHandlers;
		private readonly IUserQueryHandlers userQueryHandlers;

		public UsersController(IUserCommandHandlers userCommandHandlers, IUserQueryHandlers userQueryHandlers)
		{
			this.userCommandHandlers = userCommandHandlers;
			this.userQueryHandlers = userQueryHandlers;
		}

		// GET api/users
		[HttpGet]
		[Authorize]
		public async Task<IEnumerable<UserView>> GetAsync()
		{
			return (await userQueryHandlers.HandleAsync(new GetUsers()))
				.Select(userState => userState.ToUserView());
		}

		// POST api/users/authenticate
		[HttpPost("authenticate")]
		public async Task<UserView> AuthenticateAsync([FromBody] AuthenticateUser authenticateUser)
		{
			var (userState, token) = await userQueryHandlers.HandleAsync(authenticateUser);

			return userState
				.ToUserView(token);
		}

		// GET api/users/5
		[HttpGet("{id}")]
		public async Task<UserView> Get(int id)
		{
			return (await userQueryHandlers.HandleAsync(new GetUser { Id = id }))
				.ToUserView();
		}

		// POST api/users
		[HttpPost]
		public async Task<UserView> Post([FromBody] CreateUser createUser)
		{
			return (await userCommandHandlers.HandleAsync(createUser))
				.ToUserView();
		}

		// PUT api/users/5
		[HttpPut("{id}")]
		public async Task<UserView> Put(int id, [FromBody] UpdateUserDetails updateUserDetails)
		{
			return (await userCommandHandlers.HandleAsync(updateUserDetails))
				.ToUserView();
		}

		// PUT api/users/5
		[HttpPut("{id}/updatepassword")]
		public async Task<UserView> Put(int id, [FromBody] UpdateUserPassword updateUserPassword)
		{
			return (await userCommandHandlers.HandleAsync(updateUserPassword))
				.ToUserView();
		}

		// DELETE api/users/5
		[HttpDelete("{id}")]
		public async Task<UserView> Delete(long id, [FromBody] DeleteUser deleteUser)
		{
			return (await userCommandHandlers.HandleAsync(deleteUser))
				.ToUserView();
		}
	}
}
