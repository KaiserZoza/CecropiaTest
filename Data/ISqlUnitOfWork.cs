using Entities;
using System.Data.Entity;

namespace Data
{
    public interface ISqlUnitOfWork : IUnitOfWork
    {
        IDbSet<TEntity> GetSet<TEntity>() where TEntity : class;
        DbContext GetContext();
    }
}
