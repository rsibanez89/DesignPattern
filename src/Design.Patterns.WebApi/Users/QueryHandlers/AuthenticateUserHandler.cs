using System;
using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserQueryHandlers
	{
		public async Task<UserState> HandleAsync(AuthenticateUser msg, CancellationToken? cancellationToken)
		{
			var state = await repository.GetUserByEmail(msg.Email);
			if (state == null)
			{
				throw new ApplicationException("Email or password is incorrect");
			}

			var encryptedPassword = new EncryptedPassword
			{
				Password = state.Password,
				Salt = state.Salt
			};

			if (!passwordService.ValidatePassword(msg.Password, encryptedPassword))
			{
				throw new ApplicationException("Email or password is incorrect");
			}

			// Create authorization token

			return state;
		}
	}
}
