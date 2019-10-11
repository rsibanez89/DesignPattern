using System.Threading.Tasks;

namespace Design.Patterns.Core
{
    public interface IHandlerAsync<TState, TMessage>
    {
        Task<TState> HandleAsync(TMessage message);
    }

    public interface IHandlerAsync<TState, TMessage, TMessageContext>
	{
		Task<TState> HandleAsync(TMessage message, TMessageContext messageContext);
	}
}
