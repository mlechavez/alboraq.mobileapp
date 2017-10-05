using Alboraq.MobileApp.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.EF.Repositories
{
    internal abstract class Repository<T> : IRepository<T> where T : class
    {
        private AlboraqAppContext _ctx;
        private DbSet<T> _set;

        public Repository(AlboraqAppContext ctx)
        {
            _ctx = ctx;
        }

        protected DbSet<T> Set
        {
            get { return _set ?? (_set = _ctx.Set<T>()); }
        }

        public void Add(T entity)
        {
            Set.Add(entity);    
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Set.AddRange(entities);
        }

        public Task<List<T>> GetAllAsync()
        {
            return Set.ToListAsync();
        }

        public Task<T> GetAsync(object id)
        {
            return Set.FindAsync(id);
        }

        public void Remove(T entity)
        {
            Set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Set.RemoveRange(entities);
        }
    }
}
