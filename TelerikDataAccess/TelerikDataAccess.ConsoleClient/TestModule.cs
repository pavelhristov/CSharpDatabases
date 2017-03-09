using Ninject.Modules;
using Ninject;
using System;
using TelerikDataAccess.Models;
using Telerik.OpenAccess;

namespace TelerikDataAccess.ConsoleClient
{
    public class TestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<OpenAccessContext>().To<TestDbContext>().InSingletonScope();
            Bind(typeof(IGenericRepository<>)).To(typeof(DataAccessGenericRepository<>));
            Bind<Func<Models.IUnitOfWork>>().ToMethod(ctx=>()=>ctx.Kernel.Get<Models.IUnitOfWork>());
            Bind<Models.IUnitOfWork>().To<DataAccessUnitOfWork>();
        }
    }
}
