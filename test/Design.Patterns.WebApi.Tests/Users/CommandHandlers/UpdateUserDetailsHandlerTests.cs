using Design.Patterns.WebApi.Users;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Tests.Users
{
	[TestFixture(Category = "UserCommandHandlersTests")]
	public partial class UserCommandHandlersTests
	{
		[Test]
		public async Task UpdateUserDetailsHandlerTest()
		{
			// Given
			var updateUserDetails = new UpdateUserDetails
			{
				Email = "new@email.com",
				FirstName = "First",
				LastName = "Last",
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
				.UpdateAsync(Arg.Any<UserState>())
				.Returns(param => Task.FromResult((UserState)param[0]));

			// When
			var user = await userCommandHandlers.HandleAsync(updateUserDetails);

			// Then
			user.Email.Should().Be("new@email.com");
			user.FirstName.Should().Be("First");
			user.LastName.Should().Be("Last");
		}
	}
}
