using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelerikDataAccess.Models
{
    public interface ITestDbContext
    {
        IQueryable<Customer> Customers { get;}
    }
}
