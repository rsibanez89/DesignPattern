using Design.Patterns.WebApi.Users;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Tests.Users
{
	[TestFixture(Category = "UserCommandHandlersTests")]
	public partial class UserCommandHandlersTests
	{
		[Test]
		public async Task CreateUserHandlerTest()
		{
			// Given
			var createUser = new CreateUser
			{
				Email = "rsibanez89@gmai.com",
				FirstName = "Rodrigo",
				LastName = "Ibanez",
				Password = "12345678"
			};

			var encodedPassword = Encoding.UTF8.GetBytes("password");
			var encodedSalt = Encoding.UTF8.GetBytes("salt");

			passwordService
				.EncryptPassword(createUser.Password)
				.Returns(new EncryptedPassword { Password = encodedPassword, Salt = encodedSalt });

			repository
				.CreateAsync(Arg.Any<UserEntity>())
				.Returns(param => Task.FromResult((UserEntity)param[0]));

			// When
			var user = await userCommandHandlers.HandleAsync(createUser, null);

			// Then
			passwordService.Received(1).EncryptPassword("12345678");
			user.Email.Should().Be("rsibanez89@gmai.com");
			user.FirstName.Should().Be("Rodrigo");
			user.LastName.Should().Be("Ibanez");
			user.Password.Should().BeEquivalentTo(encodedPassword);
			user.Salt.Should().BeEquivalentTo(encodedSalt);
		}
	}
}
