using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace TelerikDataAccess.Models
{
    public class DataAccessGenericRepository<T> : IGenericRepository<T> where T : class
    {
        public DataAccessGenericRepository(OpenAccessContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            }

            this.Context = context;
        }

        public OpenAccessContext Context { get; private set; }

        public void Add(T entity)
        {
            this.Context.Add(entity);
        }

        public void Delete(T entity)
        {
            this.Context.Delete(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return this.Context.GetAll<T>();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return this.Context.GetAll<T>().FirstOrDefault(predicate);
        }

    }
}
