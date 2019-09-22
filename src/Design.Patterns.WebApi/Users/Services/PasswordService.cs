using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Design.Patterns.WebApi.Users
{
	public interface IPasswordService
	{
		EncryptedPassword EncryptPassword(string password);
		bool ValidatePassword(string password, EncryptedPassword encryptedPassword);
	}

	public class PasswordService : IPasswordService
	{
		private readonly string passwordPepper;

		public PasswordService(IOptions<UserModuleOptions> options)
		{
			this.passwordPepper = options.Value.PasswordPepper;
		}

		public EncryptedPassword EncryptPassword(string password)
		{
			// Generate random salt
			var saltBytes = new byte[32];
			RandomNumberGenerator.Create().GetBytes(saltBytes);

			var hashedPassword = GetHashedPassword(password, saltBytes);

			return new EncryptedPassword
			{
				Password = hashedPassword,
				Salt = saltBytes
			};
		}

		public bool ValidatePassword(string password, EncryptedPassword encryptedPassword)
		{
			var hashedPassword = GetHashedPassword(password, encryptedPassword.Salt);

			return hashedPassword.SequenceEqual(encryptedPassword.Password);
		}

		private byte[] GetHashedPassword(string password, byte[] saltBytes)
		{
			// Convert Password and Pepper to byte arrays
			var passwordBytes = Encoding.UTF8.GetBytes(password);
			var pepperBytes = Encoding.UTF8.GetBytes(passwordPepper);

			// Merge Salt, Password and Pepper
			var expandedPasswordBytes = new byte[saltBytes.Length + passwordBytes.Length + pepperBytes.Length];
			Array.Copy(saltBytes, 0, expandedPasswordBytes, 0, saltBytes.Length);
			Array.Copy(passwordBytes, 0, expandedPasswordBytes, saltBytes.Length, passwordBytes.Length);
			Array.Copy(pepperBytes, 0, expandedPasswordBytes, (saltBytes.Length + passwordBytes.Length), pepperBytes.Length);

			return new SHA256Managed().ComputeHash(expandedPasswordBytes);
		}

	}
}
