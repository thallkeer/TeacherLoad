using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;

namespace TeacherLoad.Data.Service
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        internal TeacherLoadContext context;
        internal DbSet<TEntity> dbSet;

        public GenericService(TeacherLoadContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }


        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsNoTracking();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new [] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached
                /*|| context.Entry(entityToDelete).State == EntityState.Unchanged*/)
            {
                dbSet.Attach(entityToDelete);
            }            
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {           
            //dbSet.Attach(entityToUpdate);            
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
