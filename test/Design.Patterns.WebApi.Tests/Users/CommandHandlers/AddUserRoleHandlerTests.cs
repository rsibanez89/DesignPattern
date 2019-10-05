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
		[TestCase(UserRole.RegisteredUser)]
		[TestCase(UserRole.Moderator)]
		[TestCase(UserRole.Admin)]
		public async Task AddUserRoleHandlerTest(UserRole userRole)
		{
			// Given
			var addRole = new AddUserRole
			{
				Role = userRole
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
			var user = await userCommandHandlers.HandleAsync(addRole);

			// Then
			user.Roles.Should().Contain(userRole);
		}
	}
}
