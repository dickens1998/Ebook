using EBook.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace EBook.Core.Infrastructure
{
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private readonly IDbContext _context;
        private DbSet<T> _entities;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDbContext context)
        {
            this._context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// Get entity by identifier async
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public Task<T> GetByIdAsync(object id)
        {
            return this.Entities.FindAsync(id);
        }

        /// <summary>
        /// Get Entities
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return this.Entities.ToList();
        }

        /// <summary>
        /// Get Entities async
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await this.Entities.ToListAsync();
        }

        /// <summary>
        /// get entities by predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        public List<T> ToList(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Entities.Where(predicate).ToList();
        }

        /// <summary>
        /// get entities by predicate async
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        public Task<List<T>> ToListAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Entities.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public T Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Add(entity);

                this._context.SaveChanges();

                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Insert entity async
        /// </summary>
        /// <param name="entity">Entity</param>
        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                await this._context.SaveChangesAsync();

                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }


                foreach (var entity in entities)
                {
                    this.Entities.Add(entity);
                }

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Insert entities async
        /// </summary>
        /// <param name="entities">Entities</param>
        public async Task<List<T>> InsertAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                foreach (var entity in entities)
                {
                    this.Entities.Add(entity);
                }

                await this._context.SaveChangesAsync();

                return entities.ToList();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public T Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this._context.Entry(entity).State = EntityState.Modified;

                this._context.SaveChanges();

                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.Aggregate(string.Empty,
                    (current1, validationErrors) => validationErrors.ValidationErrors.Aggregate(current1,
                        (current, validationError) =>
                            current +
                            ($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" +
                             Environment.NewLine)));

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Update entity async
        /// </summary>
        /// <param name="entity">Entity</param>
        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this._context.Entry(entity).State = EntityState.Modified;

                await this._context.SaveChangesAsync();

                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                foreach (var entity in entities)
                {
                    this._context.Entry(entity).State = EntityState.Modified;
                }

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Update entities async
        /// </summary>
        /// <param name="entities">Entities</param>
        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                foreach (var entity in entities)
                {
                    this._context.Entry(entity).State = EntityState.Modified;
                }

                await this._context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Remove(entity);

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Delete entity async
        /// </summary>
        /// <param name="entity">Entity</param>
        public async Task DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Remove(entity);

                await this._context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                foreach (var entity in entities)
                {
                    this.Entities.Remove(entity);
                }

                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Delete entities async
        /// </summary>
        /// <param name="entity">Entity</param>
        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }

                foreach (var entity in entities)
                {
                    this.Entities.Remove(entity);
                }

                await this._context.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                var fail = new Exception(msg, dbEx);

                throw fail;
            }
        }

        /// <summary>
        /// Get count by predicate
        /// </summary>
        /// <returns>Count</returns>
        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Entities.Count(predicate);
        }

        /// <summary>
        /// Get count by predicate async
        /// </summary>
        /// <returns>Count</returns>
        public async Task<int> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await this.Entities.CountAsync(predicate);
        }

        /// <summary>
        /// Get single entity by predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        public T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Entities.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Get single entity by predicate async
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        public async Task<T> SingleOrDefaultAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await this.Entities.SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Get single entity by predicate and include RelativePaths
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <param name="paths">relative paths</param>
        /// <returns></returns>
        public T IncludeSingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params System.Linq.Expressions.Expression<Func<T, object>>[] paths)
        {
            this.Include(paths);

            return this.Entities.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Get single entity by predicate and include RelativePaths async
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <param name="paths">relative paths</param>
        /// <returns></returns>
        public async Task<T> IncludeSingleOrDefaultAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params System.Linq.Expressions.Expression<Func<T, object>>[] paths)
        {
            IQueryable<T> query = this.Entities;

            foreach (var path in paths)
            {
                query = query.Include(path);
            }

            return await query.SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Get entities by predicate and include relative paths.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public async Task<List<T>> IncludeToListAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params System.Linq.Expressions.Expression<Func<T, object>>[] paths)
        {
            IQueryable<T> query = this.Entities;

            foreach (var path in paths)
            {
                query = query.Include(path);
            }

            return await query.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Get first entity by predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        public T FirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.Entities.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Get first entity by predicate async
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        public async Task<T> FirstOrDefaultAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await this.Entities.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Include multiple entity
        /// </summary>
        /// <param name="paths">Path</param>
        /// <returns></returns>   
        public void Include(params System.Linq.Expressions.Expression<Func<T, object>>[] paths)
        {
            foreach (var path in paths)
            {
                this.Entities.Include(path);
            }
        }

        public async Task LoadAsync(T entity, System.Linq.Expressions.Expression<Func<T, ICollection<T>>> navigationProperty)
        {
            await this._context.Entry(entity).Collection(navigationProperty).LoadAsync();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Table
        /// </summary>
        public IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }

                return _entities;
            }
        }

        #endregion
    }
}
