using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Entities
{
    public interface IGenericRepository<TEntity>
    {

        IUnitOfWork UnitOfWork { get; set; }

        IEnumerable<TEntity> Get(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeProperties = "");

        TEntity GetById(object id);
        TEntity GetById(object[] id);
        IEnumerable<TEntity> GetAll();
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        IEnumerable<TEntity> Refresh(IEnumerable<TEntity> items);
        TEntity Refresh(TEntity item);
        TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeProperties = "");
        IQueryable<TEntity> GetIQueryable(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeProperties = "");

        IQueryable<TEntity> Paged(int page, int pageSize,
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeProperties = "");

        IQueryable<T> Paged<T>(IQueryable<T> source, int page,
            int pageSize);

        IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector);

    }
}
