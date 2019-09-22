using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public class EncryptedPassword
	{
		public byte[] Password { get; set; }
		public byte[] Salt { get; set; }
	}
}
