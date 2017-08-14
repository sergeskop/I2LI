using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace I2LI.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Include or not navigation properties
        /// </summary>
        public bool IncludeNavigationProperties { get; set; }

        protected readonly DbContext _dbContext;

        protected virtual DbContext DBContext { get { return _dbContext; } }

        public GenericRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            IncludeNavigationProperties = true;
        }

        public virtual IQueryable<TEntity> AsQueriable()
        {
            return DBContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DBContext.Set<TEntity>().ToList();
        }

        public virtual TEntity Get(object id)
        {
            return DBContext.Set<TEntity>().SingleOrDefault();
        }

        public virtual IEnumerable<TEntity> FindMany(Expression<Func<TEntity, bool>> predicate)
        {
            return DBContext.Set<TEntity>().Where(predicate);
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return DBContext.Set<TEntity>().SingleOrDefault(predicate);
        }

        public virtual void Add(TEntity entity)
        {
            DBContext.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            DBContext.Set<TEntity>().AddRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            DBContext.Set<TEntity>().Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            DBContext.Set<TEntity>().RemoveRange(entities);
        }
    }
}
