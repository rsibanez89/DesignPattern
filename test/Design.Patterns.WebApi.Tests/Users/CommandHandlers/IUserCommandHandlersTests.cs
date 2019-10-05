using Design.Patterns.WebApi.Users;
using NSubstitute;
using NUnit.Framework;

namespace Design.Patterns.WebApi.Tests.Users
{
	[TestFixture(Category = "UserCommandHandlersTests")]
	public partial class UserCommandHandlersTests
	{
		private IUserRepository repository;
		private IPasswordService passwordService;
		private IUserCommandHandlers userCommandHandlers;

		[SetUp]
		public void Setup()
		{
			repository = Substitute.For<IUserRepository>();
			passwordService = Substitute.For<IPasswordService>();
			userCommandHandlers = new UserCommandHandlers(repository, passwordService);
		}
	}
}
