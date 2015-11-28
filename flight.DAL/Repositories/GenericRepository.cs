using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using flight.DAL.Contexts;
using flight.DAL.Library;


namespace flight.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BaseContext Context;

        public GenericRepository(IUnitOfWork unitOfWork, IBaseContext context)
        {
            // register this repository with the unit of work.
            unitOfWork.Register(this);

            // Because Entityframeworks dbcontext does not have an interface cast it to basecontext.
            Context = (BaseContext)context;
        }

        // generic get function with lambda expressions for repositories.
        public virtual IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = Context.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
                return orderBy(query);

            return query;
        }


        /// <summary>
        /// add the entity to the dbset
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            Context.Set(entity.GetType()).Add(entity);
        }

        // remove the entity from the dbset
        public virtual void Delete(T entity)
        {
            Context.Set(entity.GetType()).Remove(entity);
        }

        public void  Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Submit()
        {
            // saving changes
            Context.SaveChanges();
        }

        public void Dispose()
        {
            
        }

    }
}