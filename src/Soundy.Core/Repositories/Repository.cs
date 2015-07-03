﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Soundy.Data.Context;

namespace Soundy.Core.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal SoundyDb Context;
        internal DbSet<T> DbSet;

        public Repository(SoundyDb context)
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public virtual Task<IEnumerable<T>> GetAsync(
         Expression<Func<T, bool>> filter = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         string includeProperties = "")
        {
            return Task.Run(() => Get(filter, orderBy, includeProperties));
        }



        public virtual T Get(object id)
        {
            return DbSet.Find(id);
        }
        public virtual Task<T> GetAsync(object id)
        {
            return DbSet.FindAsync(id);
        }


        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }
        public virtual void Update(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void Delete(object id)
        {
            Delete(DbSet.Find(id));
        }
        public virtual void Delete(T entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }



        public virtual bool Exists(int id)
        {
            return DbSet.Find(id) != null;
        }
        public async virtual Task<bool> ExistsAsync(int id)
        {
            return await DbSet.FindAsync(id) != null;
        }

        public int Save()
        {
            return Context.SaveChanges();
        }
        public Task SaveAsync()
        {
            return Context.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
            Context.Dispose();
        }

        ~Repository()
        {
            Dispose();
        }
    }
}
