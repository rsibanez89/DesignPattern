using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Design.Patterns.WebApi.Users
{
	public class UserRoleTypeHandler : SqlMapper.TypeHandler<IEnumerable<UserRole>>
	{
		public override IEnumerable<UserRole> Parse(object roles)
		{
			return roles.ToString().Split(",").Select(role => Enum.Parse<UserRole>(role));
		}

		public override void SetValue(IDbDataParameter parameter, IEnumerable<UserRole> roles)
		{
			parameter.Value = string.Join(", ", roles);
		}
	}
}
