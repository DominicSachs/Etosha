using Microsoft.AspNetCore.Identity;

namespace Etosha.Server.Entities
{
	public class AppRole : IdentityRole<int>
	{
		public AppRole() { }

		public AppRole(string roleName) : base(roleName) { }
	}
}
