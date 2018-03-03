namespace Entities
{
    public interface IUnitOfWork
    {
        int Commit();

        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
