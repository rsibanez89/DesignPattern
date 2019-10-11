using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Design.Patterns.Core
{
	public interface IRepository<TEntity, TMessageContext>
		where TEntity : Entity
		where TMessageContext : MessageContext
	{
		/// <summary>
		/// Returns the list of Entities
		/// </summary>
		/// <param name="entityId"></param>
		/// <returns></returns>
		Task<IEnumerable<TEntity>> ListAsync(TMessageContext context = null);

		/// <summary>
		/// Returns the last state of the Entity or throw an exception if the Entity does not exist
		/// </summary>
		/// <param name="entityId"></param>
		/// <returns></returns>
		Task<TEntity> GetAsync(long entityId, TMessageContext context = null);

		/// <summary>
		/// Creates a new instance of the Entity in the database
		/// </summary>
		/// <param name="entityId"></param>
		/// <returns></returns>
		Task<TEntity> CreateAsync(TEntity entityId, TMessageContext context = null);

		/// <summary>
		/// Updates the Entity in the database
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task<TEntity> UpdateAsync(TEntity entity, TMessageContext context = null);

		/// <summary>
		/// Deletes the Entity in the database
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task<TEntity> DeleteAsync(TEntity entity, TMessageContext context = null);
	}
}
