using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Data
{
    public class GenericSqlRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private ISqlUnitOfWork _unitOfWork;
        private IDbSet<TEntity> _dbSet;

        public GenericSqlRepository(ISqlUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException("unitOfWork");
            _dbSet = _unitOfWork.GetSet<TEntity>();
        }

        #region IGenericSlqRepository<TEntity> Members

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
            set
            {
                _unitOfWork = (ISqlUnitOfWork)value;
                _dbSet = _unitOfWork.GetSet<TEntity>();
            }
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();

        }

        public IEnumerable<TEntity> Refresh(IEnumerable<TEntity> items)
        {
            var context = ((IObjectContextAdapter)_unitOfWork.GetContext());
            context.ObjectContext.Refresh(RefreshMode.StoreWins, items);
            return items;
        }

        public TEntity Refresh(TEntity item)
        {
            var context = ((IObjectContextAdapter)_unitOfWork.GetContext());
            context.ObjectContext.Refresh(RefreshMode.StoreWins, item);
            return item;
        }

        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public TEntity GetById(object[] id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Insert(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var context = _unitOfWork.GetContext();
            context.Set<TEntity>().Attach(entity);
            _dbSet.Add(entity);
        }

        public void Delete(object id)
        {
            if (id == null) throw new ArgumentNullException("id");
            TEntity entityToDelete = _dbSet.Find(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null) throw new ArgumentNullException("entityToDelete");
            var context = _unitOfWork.GetContext();
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }



        public void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null) throw new ArgumentNullException("entityToUpdate");
            var context = _unitOfWork.GetContext();
            if (context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToUpdate);

            }
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {

            return Get(filter, orderBy, includeProperties).FirstOrDefault();
        }

        public IQueryable<TEntity> GetIQueryable(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query) : query;

        }


        public IQueryable<TEntity> Paged(int page, int pageSize,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy(query).Skip((page) * pageSize).Take(pageSize);
        }

        public IQueryable<T> Paged<T>(IQueryable<T> source, int page,
            int pageSize)
        {
            return source
                .Skip((page) * pageSize)
                .Take(pageSize);
        }

        public IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        #endregion
    }
}
