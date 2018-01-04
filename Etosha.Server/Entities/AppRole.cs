using Microsoft.AspNetCore.Identity;
using System;

namespace Etosha.Server.Entities
{
	public class AppRole : IdentityRole<Guid>
	{
		public AppRole() { }

		public AppRole(string roleName) : base(roleName) { }
	}
}
