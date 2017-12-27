using System;

namespace Etosha.Server.Entities
{
	public class Advise : BaseEntity
	{
		public string Code { get; set; }

		public string Description { get; set; }

		public Guid CustomerId { get; set; }

		public Customer Customer { get; set; }
	}
}
