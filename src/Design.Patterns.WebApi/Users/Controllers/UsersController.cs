using Design.Patterns.Core;
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
		public async Task<IEnumerable<UserView>> GetAsync()
		{
			return (await userQueryHandlers.HandleAsync(new GetUsers()))
				.Select(userState => userState.ToUserView());
		}

		// POST api/users/authenticate
		[HttpPost("authenticate")]
		public async Task<UserView> AuthenticateAsync([FromBody] AuthenticateUser authenticateUser)
		{
			return (await userQueryHandlers.HandleAsync(authenticateUser))
				.ToUserView();
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
		public async Task Post([FromBody] CreateUser createUser)
		{
			await userCommandHandlers.HandleAsync(createUser);
		}

		// PUT api/users/5
		[HttpPut("{id}")]
		public async Task Put(int id, [FromBody] UpdateUserDetails updateUserDetails)
		{
			await userCommandHandlers.HandleAsync(updateUserDetails);
		}

		// PUT api/users/5
		[HttpPut("{id}/updatepassword")]
		public async Task Put(int id, [FromBody] UpdateUserPassword updateUserPassword)
		{
			await userCommandHandlers.HandleAsync(updateUserPassword);
		}

		// DELETE api/users/5
		[HttpDelete("{id}")]
		public async Task Delete(long id)
		{
			await userCommandHandlers.HandleAsync(new DeleteUser { Id = id });
		}
	}
}
