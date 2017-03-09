using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace TelerikDataAccess.Models
{
    public class DataAccessUnitOfWork: IUnitOfWork
    {
        private readonly OpenAccessContext dbContext;

        public DataAccessUnitOfWork(OpenAccessContext dbContext)
        {
            this.dbContext = dbContext;
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
