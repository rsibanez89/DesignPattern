using System;
using System.Collections.Generic;
using System.Text;

namespace Design.Patters.Core
{
	public abstract class State
	{
		public abstract long Id { get; set; }
		
		public int Version { get; set; }

		public long LastModifiedBy { get; set; }
		public DateTime LastModifiedOnUtc { get; set; }

		public long CreatedBy { get; set; }
		public DateTime CreatedOnUtc { get; set; }

		public long? DeletedBy { get; set; }
		public DateTime? DeletedOnUTC { get; set; }
	}
}
