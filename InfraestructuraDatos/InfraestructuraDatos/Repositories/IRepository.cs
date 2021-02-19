using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InfraestructuraDatos.Repositories
{
    /// <summary>
    /// Interfaz para repositorio generico
    /// </summary>
    /// <typeparam name="T">Objeto del repositorio</typeparam>
    interface IRepository<T> where T : class
    {
        /// <summary>
        /// Obtiene los datos del repositorio basado en una expresión
        /// </summary>
        /// <param name="filter">Filtro</param>
        /// <param name="orderBy">Criterio de ordenamiento</param>
        /// <param name="includeProperties">Indica si incluye alguna propiedad de la entidad</param>
        /// <returns></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        /// <summary>
        /// Obtiene una entidad por su identificador
        /// </summary>
        /// <param name="idEntity">Id de la entidad</param>
        /// <returns></returns>
        T GetById(long idEntity);
        /// <summary>
        /// Inserta una nueva entidad
        /// </summary>
        /// <param name="entity">Entidad</param>
        void Insert(T entity);
        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="idEntity">Id de la entidad</param>
        void Delete(long idEntity);
        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <param name="entity">Entidad con los cambios</param>
        void Update(T entity);
    }
}
