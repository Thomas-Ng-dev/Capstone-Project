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
        }
        public void Add(T entity)
        {
            databaseSet.Add(entity);
        }
        public T Get(Expression<Func<T, bool >> filter)
        {
            IQueryable<T> query = databaseSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = databaseSet;
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
