using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Etosha.Server.Infrastructure
{
	// used from https://www.ryadel.com/en/c-sharp-random-password-generator-asp-net-core-mvc/
	internal static class PasswordGenerator
	{
		internal static string GenerateRandomPassword(PasswordOptions options = null)
		{
			if (options == null)
			{
				options = new PasswordOptions()
				{
					RequiredLength = 8,
					RequiredUniqueChars = 4,
					RequireDigit = true,
					RequireLowercase = true,
					RequireNonAlphanumeric = true,
					RequireUppercase = true
				};
			}

			string[] randomChars = new[] {
				"ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
				"abcdefghijkmnopqrstuvwxyz",    // lowercase
				"0123456789",                   // digits
				"!@$?_-"                        // non-alphanumeric
			};

			var rand = new Random(Environment.TickCount);
			var chars = new List<char>();

			if (options.RequireUppercase)
			{
				chars.Insert(rand.Next(0, chars.Count), randomChars[0][rand.Next(0, randomChars[0].Length)]);
			}

			if (options.RequireLowercase)
			{
				chars.Insert(rand.Next(0, chars.Count), randomChars[1][rand.Next(0, randomChars[1].Length)]);
			}

			if (options.RequireDigit)
			{
				chars.Insert(rand.Next(0, chars.Count), randomChars[2][rand.Next(0, randomChars[2].Length)]);
			}

			if (options.RequireNonAlphanumeric)
			{
				chars.Insert(rand.Next(0, chars.Count), randomChars[3][rand.Next(0, randomChars[3].Length)]);
			}

			for (int i = chars.Count; i < options.RequiredLength || chars.Distinct().Count() < options.RequiredUniqueChars; i++)
			{
				var rcs = randomChars[rand.Next(0, randomChars.Length)];
				chars.Insert(rand.Next(0, chars.Count), rcs[rand.Next(0, rcs.Length)]);
			}

			return new string(chars.ToArray());
		}
	}
}
