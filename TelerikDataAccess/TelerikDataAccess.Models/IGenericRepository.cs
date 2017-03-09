using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TelerikDataAccess.Models
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        void Add(T entity);

        void Delete(T entity);


        T Get(Expression<Func<T, bool>> predicate);
    }
}
