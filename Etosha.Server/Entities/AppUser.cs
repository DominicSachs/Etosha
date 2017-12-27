using Microsoft.AspNetCore.Identity;
using System;

namespace Etosha.Server.Entities
{
	public class AppUser : IdentityUser<Guid>
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}
