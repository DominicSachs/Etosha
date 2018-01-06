using Microsoft.AspNetCore.Identity;

namespace Etosha.Server.Entities
{
	public class AppUser : IdentityUser<int>
	{
		public AppUser() : this(null) { }

		public AppUser(string userName) : this(userName, null, null, null) { }

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
