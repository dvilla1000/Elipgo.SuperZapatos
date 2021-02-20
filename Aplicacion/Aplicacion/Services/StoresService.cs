using AutoMapper;
using Elipgo.SuperZapatos.Aplicacion.Adaptadores;
using Elipgo.SuperZapatos.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elipgo.SuperZapatos.Aplicacion.Services
{
    /// <summary>
    /// Clase de Servicio para los stores
    /// </summary>
    public class StoresService
    {
        private InfraestructuraDatos.Data.SuperZapatosDBContext szDBContext = new InfraestructuraDatos.Data.SuperZapatosDBContext();

        /// <summary>
        /// Obtiene una colección completa de stores
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Adaptadores.Store> GetStores()
        {
            IRepository<Dominio.Entities.Store> storesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Store>(szDBContext);
            List<Dominio.Entities.Store> stores = storesRepository.Get().ToList();                
            List<Adaptadores.Store> storesDTO = ObjectMapper.Mapper.MapList<Dominio.Entities.Store, Adaptadores.Store>(stores);
            return storesDTO;
        }

        /// <summary>
        /// Obtiene un Store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Adaptadores.Store GetStore(long id)
        {
            IRepository<Dominio.Entities.Store> storesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Store>(szDBContext);
            Dominio.Entities.Store store = storesRepository.GetById(id);
            Adaptadores.Store storeDTO = ObjectMapper.Mapper.Map<Dominio.Entities.Store, Adaptadores.Store>(store);
            return storeDTO;
        }

        /// <summary>
        /// Realiza una adición de un store en el repositorio
        /// </summary>
        /// <param name="store"></param>
        public void AddStore(Store store)
        {
            IRepository<Dominio.Entities.Store> storesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Store>(szDBContext);
            Dominio.Entities.Store storeEntity = ObjectMapper.Mapper.Map<Adaptadores.Store, Dominio.Entities.Store>(store);
            storesRepository.Insert(storeEntity);            
        }

        /// <summary>
        /// Realiza una actualización en el repositorio
        /// </summary>
        /// <param name="store"></param>
        public void UpdateStore(Store store)
        {
            IRepository<Dominio.Entities.Store> storesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Store>(szDBContext);
            Dominio.Entities.Store storeEntity = ObjectMapper.Mapper.Map<Adaptadores.Store, Dominio.Entities.Store>(store);
            storesRepository.Update(storeEntity);
        }

        /// <summary>
        /// Elimina un store en el repositorio
        /// </summary>
        /// <param name="id"></param>
        public void DeleteStore(long id)
        {
            IRepository<Dominio.Entities.Store> storesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Store>(szDBContext);
            storesRepository.Delete(id);
        }

        /// <summary>
        /// Persiste los cambios
        /// </summary>
        public void SaveChanges()
        {            
            szDBContext.SaveChanges();
        }

    }
}
