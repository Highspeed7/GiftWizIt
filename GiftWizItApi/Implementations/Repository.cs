using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext Context;

        public Repository(ApplicationDbContext context) 
            => Context = context;

        public void Add(T entity) 
            => Context.Set<T>().Add(entity);

        public IEnumerable<T> Get() 
            => Context.Set<T>().ToList();

        public T GetById(Guid id) 
            => Context.Set<T>().Find(id);

        public void Remove(Guid id)
        {
            var type = Context.Set<T>().Find(id);

            Context.Remove(type);
        }
    }
}
