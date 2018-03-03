using Entities;
using System.Data.Entity;
namespace Data
{
    public class CecropiaTestContext: DbContext, ISqlUnitOfWork
    {
        public CecropiaTestContext():base("CecropiaTest")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.CommandTimeout = 300;
        }
        public IDbSet<Patient> Patients { get; set; }
        public IDbSet<Country> Counties { get; set; } 
        public IDbSet<BloodType> BloodTypes { get; set; }
        public IDbSet<ExceptionRecord> ExceptionRecords { get; set; }
        #region ISqlUnitOfWork Members

        public int Commit()
        {
            int rowsafected = base.SaveChanges();
            return rowsafected;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericSqlRepository<TEntity>(this);
        }

        public IDbSet<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public DbContext GetContext()
        {
            return this;
        }

        #endregion
    }
}
