using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Design.Patterns.Core
{
	public interface IRepository<TState>
		where TState : State
	{
		/// <summary>
		/// Returns the list of Entities
		/// </summary>
		/// <param name="entityId"></param>
		/// <returns></returns>
		Task<IEnumerable<TState>> ListAsync();

		/// <summary>
		/// Returns the last state of the Entity or throw an exception if the Entity does not exist
		/// </summary>
		/// <param name="entityId"></param>
		/// <returns></returns>
		Task<TState> GetAsync(long entityId);

		/// <summary>
		/// Creates a new instance of the Entity in the database
		/// </summary>
		/// <param name="entityId"></param>
		/// <returns></returns>
		Task<TState> CreateAsync(TState entityId);

		/// <summary>
		/// Updates the Entity in the database
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		Task<TState> UpdateAsync(TState state);

		/// <summary>
		/// Deletes the Entity in the database
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		Task<TState> DeleteAsync(TState state);
	}
}
