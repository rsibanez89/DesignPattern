using Design.Patterns.WebApi.Users;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Tests.Users
{
	[TestFixture(Category = "UserCommandHandlersTests")]
	public partial class UserCommandHandlersTests
	{
		[Test]
		public async Task DeleteUserHandlerTest()
		{
			// Given
			var deleteUser = new DeleteUser
			{
			};

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
				.DeleteAsync(Arg.Any<UserState>())
				.Returns(param => Task.FromResult((UserState)param[0]));


			// When
			var user = await userCommandHandlers.HandleAsync(deleteUser);

			// Then
			await repository.Received(1).DeleteAsync(Arg.Any<UserState>());
		}
	}
}
