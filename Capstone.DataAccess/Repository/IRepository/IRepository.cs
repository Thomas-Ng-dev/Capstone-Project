using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DataAccess.Repository.IRepository
{
    //Pattern where we can put any model "T" and give a set 
    //of common actions through the interface
    public interface IRepository<T> where T : class
    {
        //Retrieve list from database and store in collection
        IEnumerable<T> GetAll(string? includeProperties = null);
        //Retrieve 1 element
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
