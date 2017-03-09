using SuperheroesUniverse.Data.Contracts;
using SuperheroesUniverse.Data.Repositories.Contracts;
using System.Data.Entity;

namespace SuperheroesUniverse.Data.Repositories
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly IDbContext dbContext;

        public EntityFrameworkUnitOfWork(IDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbContext.CreateDatabaseIfNotExists();
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
