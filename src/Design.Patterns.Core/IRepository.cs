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
		/// Creates a new instance of the Entity
		/// </summary>
		/// <param name="entityId"></param>
		/// <returns></returns>
		Task<TState> CreateAsync(long entityId);

		/// <summary>
		/// Returns the last state of the Entity or Creates a new instance if the Entity does not exist
		/// </summary>
		/// <param name="entityId"></param>
		/// <returns></returns>
		Task<TState> GetOrCreateAsync(long entityId);

		/// <summary>
		/// Saves the Entity into the database
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		Task<TState> SaveChangesAsync(TState state);
	}
}
