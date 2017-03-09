using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using TelerikDataAccess.Models;

namespace TelerikDataAccess.ConsoleClient
{
    class StartUp
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new TestModule());

            UpdateDatabase();
            var dp = kernel.Get<DataProvider>();

            using (var unitOfWork = dp.UnitOfWork())
            {
                //dp.Customers.Add(new Customer()
                //{
                //    ID = 2,
                //    Name = "Gosho",
                //    EmailAddress = "no@email.com",
                //});

                //unitOfWork.Commit();

                foreach( var customer in dp.Customers.GetAll())
                {
                    Console.WriteLine(customer.Name);
                }
            }


            //var unitOfWork = kernel.Get<Models.IUnitOfWork>();

            //using (unitOfWork)
            //{
            //    var repository = kernel.Get(IGenericRepository);
            //}
        }

        private static void UpdateDatabase()
        {
            using (var context = new TestDbContext())
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureDB(schemaHandler);
            }
        }

        private static void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }
    }
}
