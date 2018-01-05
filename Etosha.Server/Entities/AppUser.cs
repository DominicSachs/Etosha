using Microsoft.AspNetCore.Identity;
using System;

namespace Etosha.Server.Entities
{
	public class AppUser : IdentityUser<Guid>
	{
		public AppUser() : this(null) { }

		public AppUser(string userName) : this(userName, null, null, null) { }

		public AppUser(string userName, string firstName, string lastName, string email) : base(userName)
		{
			Id = Guid.NewGuid();
			FirstName = firstName;
			LastName = lastName;
			Email = email;
		}

		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}
