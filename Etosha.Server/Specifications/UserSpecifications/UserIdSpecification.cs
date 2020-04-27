using System;
using System.Linq.Expressions;
using Etosha.Server.Entities;

namespace Etosha.Server.Specifications.UserSpecifications
{
	public class UserIdSpecification : Specification<AppUser>
	{
		private readonly int _userId;

		public UserIdSpecification(int userId)
		{
			_userId = userId;
		}

		public override Expression<Func<AppUser, bool>> ToExpression()
		{
			return user => user.Id == _userId;
		}
	}
}
