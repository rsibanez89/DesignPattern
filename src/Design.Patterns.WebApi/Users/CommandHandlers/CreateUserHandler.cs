using Design.Patterns.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserCommandHandlers
	{
		public async Task<UserEntity> HandleAsync(CreateUser msg, MessageContext messageContext)
		{
			var encryptedPassword = passwordService.EncryptPassword(msg.Password);
			var state = new UserEntity
			{
				FirstName = msg.FirstName,
				LastName = msg.LastName,
				Email = msg.Email,
				Password = encryptedPassword.Password,
				Salt = encryptedPassword.Salt,
				Roles = new List<UserRole>() { UserRole.RegisteredUser }
			};

			return await repository.CreateAsync(state);
		}
	}
}
