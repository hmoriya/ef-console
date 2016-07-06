using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApplication
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Find(params object[] keyValues);

        IList<TEntity> ListAll();

        IList<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Remove(TEntity entity);

        void Save();
    }
    public abstract class Repository<TContext, TEntity> : IRepository<TEntity>
                where TContext : DbContext, new()
                where TEntity : class
    {

        protected Repository(TContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        protected Repository() : this(new TContext())
        {
        }

        private readonly DbContext context;
        private readonly DbSet<TEntity> dbSet;

        public TEntity Find(params object[] keyValues)
        {
            // Find method not implementd EF Core rc2
            throw new NotImplementedException();
            // return dbSet.Find(keyValues);
        }

        public IList<TEntity> ListAll()
        {
            return dbSet.ToList();
        }

        public IList<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}