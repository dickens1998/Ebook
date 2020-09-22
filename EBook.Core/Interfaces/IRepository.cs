using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EBook.Core.Interfaces
{
    public partial interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T GetById(object id);
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Get entities
        /// </summary>
        /// <returns>Entities</returns>
        List<T> GetAll();
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// get entities by predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        List<T> ToList(Expression<Func<T, bool>> predicate);
        Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        T Insert(T entity);
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Insert(IEnumerable<T> entities);
        Task<List<T>> InsertAsync(IEnumerable<T> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        //void UpdateAsync(EBook.Dtos.Books.BookDto books);

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<T> entities);
        Task UpdateAsync(IEnumerable<T> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);
        Task DeleteAsync(T entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<T> entities);
        Task DeleteAsync(IEnumerable<T> entities);

        /// <summary>
        /// Get count by predicate
        /// </summary>
        /// <returns>Count</returns>
        int Count(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get single entity by predicate and include RelativePaths.
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <param name="paths">relative paths</param>
        /// <returns></returns>
        T IncludeSingleOrDefault(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        Task<T> IncludeSingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);

        /// <summary>
        /// Get entities by predicate and include relative paths.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        Task<List<T>> IncludeToListAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);

        /// <summary>
        /// Get single entity by predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get first entity by predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Include multiple
        /// </summary>
        /// <param name="paths">Path</param>
        /// <returns></returns>        
        void Include(params Expression<Func<T, object>>[] paths);
    }
}
