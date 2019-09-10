using System;

namespace Design.Patterns.Core
{
	public abstract class State
	{
		public abstract long Id { get; set; }
		
		public int Version { get; set; }

		public long LastModifiedBy { get; set; }
		public DateTime LastModifiedOn { get; set; }

		public long CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }

		public long? DeletedBy { get; set; }
		public DateTime? DeletedOn { get; set; }
	}
}
