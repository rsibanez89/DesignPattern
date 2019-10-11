using Design.Patterns.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public partial class UserCommandHandlers
	{
		public async Task<UserEntity> HandleAsync(UpdateUserPassword msg, MessageContext messageContext)
		{
			var state = await repository.GetAsync(msg.Id);
			var encryptedPassword = new EncryptedPassword
			{
				Password = state.Password,
				Salt = state.Salt
			};

			if (!passwordService.ValidatePassword(msg.OldPassword, encryptedPassword))
			{
				throw new ApplicationException("Your old password doesn't match");
			}

			var newEncryptedPassword = passwordService.EncryptPassword(msg.NewPassword);
			state.Password = newEncryptedPassword.Password;
			state.Salt = newEncryptedPassword.Salt;
			state.Version = msg.Version;

			return await repository.UpdateAsync(state);
		}
	}
}
