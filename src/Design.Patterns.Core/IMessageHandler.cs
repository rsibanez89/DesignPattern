using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Design.Patterns.Core
{
	public interface IHandlerAsync<TState, TMessage>
	{
		Task<TState> HandleAsync(TMessage msg, CancellationToken? cancellationToken = null);
	}
}
