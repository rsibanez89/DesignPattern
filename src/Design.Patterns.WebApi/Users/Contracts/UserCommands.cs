using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public class CreateUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}

	public class UpdateUserDetails
	{
		public long Id { get; set; }
		public int Version { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}

	public class UpdateUserPassword
	{
		public long Id { get; set; }
		public int Version { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}

	public class AddUserRole
	{
		public long Id { get; set; }
		public int Version { get; set; }
		public UserRole Role { get; set; }
	}

	public class DeleteUser
	{
		public long Id { get; set; }
		public int Version { get; set; }
	}
}
