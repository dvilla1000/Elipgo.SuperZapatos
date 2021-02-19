using Elipgo.SuperZapatos.InfraestructuraDatos.Data;
using Elipgo.SuperZapatos.Dominio.Contratos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Elipgo.SuperZapatos.InfraestructuraDatos.Repositories
{
    /// <summary>
    /// Repositorio generico
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private SuperZapatosDBContext context;
        private DbSet<TEntity> dbSet;

        public GenericRepository(SuperZapatosDBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual void Delete(long idEntity)
        {
            TEntity entityToDelete = dbSet.Find(idEntity);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }


        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                query = query.Where(filter);


            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return orderBy(query).ToList();

            return query.ToList();

        }

        public virtual TEntity GetById(long idEntity)
        {
            return dbSet.Find(idEntity);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

    }
}
