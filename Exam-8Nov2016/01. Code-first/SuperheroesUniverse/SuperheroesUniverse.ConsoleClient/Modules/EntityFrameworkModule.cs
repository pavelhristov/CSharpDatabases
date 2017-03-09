using Ninject;
using Ninject.Modules;
using SuperheroesUniverse.Data;
using SuperheroesUniverse.Data.Contracts;
using SuperheroesUniverse.Data.Repositories;
using SuperheroesUniverse.Data.Repositories.Contracts;
using System;

namespace SuperheroesUniverse.ConsoleClient.Modules
{
    public class EntityFrameworkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbContext>().To<EntityFrameworkDbContext>().InSingletonScope();
            Bind(typeof(IGenericRepository<>)).To(typeof(EntityFrameworkGenericRepository<>));
            Bind<Func<IUnitOfWork>>().ToMethod(ctx => () => ctx.Kernel.Get<IUnitOfWork>());
            Bind<IUnitOfWork>().To<EntityFrameworkUnitOfWork>();
        }
    }
}
