using Capstone.DataAccess.Data;
using Capstone.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _database;
        internal DbSet<T> databaseSet;
        public Repository(ApplicationDbContext db)
        {
            _database = db;
            this.databaseSet = db.Set<T>();
            // Populate FK relation, otherwise it loads as null. Use include properties from EF core.
            _database.Products.Include(x => x.Customer).Include(x => x.CustomerId);
        }
        public void Add(T entity)
        {
            databaseSet.Add(entity);
        }
        public T Get(Expression<Func<T, bool >> filter, string? includeProperties = null)
        {
            IQueryable<T> query = databaseSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = databaseSet;
            if(!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var property in includeProperties.Split(new char[] { ',' }, 
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            databaseSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            databaseSet.RemoveRange(entity);
        }
    }
}
