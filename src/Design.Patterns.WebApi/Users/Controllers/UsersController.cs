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
		public async Task<IEnumerable<UserState>> GetAsync()
		{
			return await userQueryHandlers.HandleAsync(null, new GetUsers());
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/users
		[HttpPost]
		public async Task Post([FromBody] CreateUser createUser)
		{
			await userCommandHandlers.HandleAsync(new UserState(), createUser);
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
