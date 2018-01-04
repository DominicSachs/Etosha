using Microsoft.AspNetCore.Identity;
using System;

namespace Etosha.Server.Entities
{
	public class AppUser : IdentityUser<Guid>
	{
		public AppUser() { }

		public AppUser(string userName) : base(userName) { }

		public AppUser(string userName, string firstName, string lastName, string email) : base(userName)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
		}

		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}
