using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserCommandHandlers
	{
		public async Task<UserState> HandleAsync(CreateUser msg, CancellationToken? cancellationToken)
		{
			var encryptedPassword = passwordService.EncryptPassword(msg.Password);
			var state = new UserState
			{
				FirstName = msg.FirstName,
				LastName = msg.LastName,
				Email = msg.Email,
				Password = encryptedPassword.Password,
				Salt = encryptedPassword.Salt
			};

			return await repository.CreateAsync(state);
		}
	}
}
