using System;

namespace SuperheroesUniverse.Data.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
