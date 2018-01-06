using System;

namespace Etosha.Server.Entities
{
	public class Advise : BaseEntity
	{
		public string Description { get; set; }

		public States State { get; set; }

		public int CustomerId { get; set; }

		public Customer Customer { get; set; }

		public Guid Code { get; set; }
	}
}
