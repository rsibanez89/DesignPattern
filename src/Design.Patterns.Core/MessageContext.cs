using System;

namespace Design.Patterns.Core
{
    /// <summary>
    /// The Message Context is information that improves the undestanding of the message
    /// </summary>
	public class MessageContext
	{
		public long UserId { get; set; }
        public DateTime DateTimeStamp { get; set; }
    }
}
