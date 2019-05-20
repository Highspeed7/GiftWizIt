using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftWizItApi.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;

        public Repository(ApplicationDbContext context) 
            => Context = context;

        public void Add(T entity) 
            => Context.Set<T>().Add(entity);

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public T GetById(int id) 
            => Context.Set<T>().Find(id);

        public void Remove(int id, int? id2)
        {
            T type;

            if(id2 != null)
            {
                type = Context.Set<T>().Find(id, id2);
            }else
            {
                type = Context.Set<T>().Find(id);
            }

            Context.Remove(type);
        }
    }
}
