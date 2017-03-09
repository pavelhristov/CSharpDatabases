using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelerikDataAccess.Models;

namespace TelerikDataAccess.ConsoleClient
{
    public class DataProvider
    {
        private IGenericRepository<Customer> customers;
        private Func<IUnitOfWork> unitOfWork;

        public DataProvider(Func<IUnitOfWork> unitOfWork,IGenericRepository<Customer> customers)
        {
            this.Customers = customers;
            this.UnitOfWork = unitOfWork;
        }

        public IGenericRepository<Customer> Customers
        {
            get
            {
                return customers;
            }

            set
            {
                customers = value;
            }
        }

        public Func<IUnitOfWork> UnitOfWork
        {
            get
            {
                return unitOfWork;
            }

            set
            {
                unitOfWork = value;
            }
        }
    }
}
