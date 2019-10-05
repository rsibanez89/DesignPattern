using Design.Patterns.WebApi.Users;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Tests.Users
{
	[TestFixture(Category = "UserCommandHandlersTests")]
	public partial class UserCommandHandlersTests
	{
		[Test]
		public async Task UpdateUserPasswordHandlerTest()
		{
			// Given
			var updateUserPassword = new UpdateUserPassword
			{
				OldPassword = "12345678",
				NewPassword = "87654321"
			};

			var encodedPassword = Encoding.UTF8.GetBytes("password");
			var encodedSalt = Encoding.UTF8.GetBytes("salt");

			passwordService
				.ValidatePassword(updateUserPassword.OldPassword, Arg.Any<EncryptedPassword>())
				.Returns(true);

			passwordService
				.EncryptPassword(updateUserPassword.NewPassword)
				.Returns(new EncryptedPassword { Password = encodedPassword, Salt = encodedSalt });

			repository
				.GetAsync(Arg.Any<long>())
				.Returns(new UserState
				{
					Email = "rsibanez89@gmai.com",
					FirstName = "Rodrigo",
					LastName = "Ibanez",
					Roles = new UserRole[] { }
				});

			repository
				.UpdateAsync(Arg.Any<UserState>())
				.Returns(param => Task.FromResult((UserState)param[0]));

			// When
			var user = await userCommandHandlers.HandleAsync(updateUserPassword);

			// Then
			user.Password.Should().BeEquivalentTo(encodedPassword);
		}

		[Test]
		public void UpdateUserPasswordHandlerTest_Should_Throw_When_Passwords_Do_Not_Match()
		{
			// Given
			var updateUserPassword = new UpdateUserPassword
			{
				OldPassword = "12345678",
				NewPassword = "87654321"
			};

			var encodedPassword = Encoding.UTF8.GetBytes("password");
			var encodedSalt = Encoding.UTF8.GetBytes("salt");

			passwordService
				.ValidatePassword(updateUserPassword.OldPassword, Arg.Any<EncryptedPassword>())
				.Returns(false);

			repository
				.GetAsync(Arg.Any<long>())
				.Returns(new UserState
				{
					Email = "rsibanez89@gmai.com",
					FirstName = "Rodrigo",
					LastName = "Ibanez",
					Roles = new UserRole[] { }
				});

			// When
			// Then
			Assert.ThrowsAsync<ApplicationException>(() => userCommandHandlers.HandleAsync(updateUserPassword))
				.Message.Should().Be("Your old password doesn't match");
		}
	}
}
