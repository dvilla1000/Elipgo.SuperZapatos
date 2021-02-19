using AutoMapper;
using Elipgo.SuperZapatos.Aplicacion.Adaptadores;
using Elipgo.SuperZapatos.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elipgo.SuperZapatos.Aplicacion.Services
{
    public class StoresService
    {
        private InfraestructuraDatos.Data.SuperZapatosDBContext szDBContext = new InfraestructuraDatos.Data.SuperZapatosDBContext();
        //private IMapper _iMapper;

        public StoresService()
        {
            //var config = new MapperConfiguration(cfg =>
            //{
            //    //cfg.CreateMap<Dominio.Entities.Store, Adaptadores.Store>().ForMember(dto => dto.Id, map => map.MapFrom(source => new Currency
            //    //{
            //    //    Code = source.CurrencyCode,
            //    //    Value = source.CurrencyValue.ToString("0.00")
            //    //}));
            //    cfg.CreateMap<Dominio.Entities.Store, Adaptadores.Store>();
            //        //.ForMember(dto =>
            //        //    dto.Id,
            //        //    opt => opt.MapFrom(src => src.Id))
            //        //.ForMember(dest =>
            //        //    dest.Name,
            //        //    opt => opt.MapFrom(src => src.Name))
            //        //.ForMember(
            //        //    dest => dest.Address, 
            //        //    opt => opt.MapFrom(src => src.Address));
            //});

            //var dto = ObjectMapper.Mapper.Map<DtoClass>(entity);

            //_iMapper = config.CreateMapper();

        }

        public IEnumerable<Adaptadores.Store> GetStores()
        {
            IRepository<Dominio.Entities.Store> storesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Store>(szDBContext);
            List<Dominio.Entities.Store> stores = storesRepository.Get().ToList();                
            List<Adaptadores.Store> storesDTO = ObjectMapper.Mapper.MapList<Dominio.Entities.Store, Adaptadores.Store>(stores);
            return storesDTO;
        }

        public Adaptadores.Store GetStore(long id)
        {
            IRepository<Dominio.Entities.Store> storesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Store>(szDBContext);
            Dominio.Entities.Store store = storesRepository.GetById(id);
            Adaptadores.Store storeDTO = ObjectMapper.Mapper.Map<Dominio.Entities.Store, Adaptadores.Store>(store);
            return storeDTO;
        }

        public void AddStore(Store store)
        {
            IRepository<Dominio.Entities.Store> storesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Store>(szDBContext);
            Dominio.Entities.Store storeEntity = ObjectMapper.Mapper.Map<Adaptadores.Store, Dominio.Entities.Store>(store);
            storesRepository.Insert(storeEntity);            
        }

        public void UpdateStore(Store store)
        {
            IRepository<Dominio.Entities.Store> storesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Store>(szDBContext);
            Dominio.Entities.Store storeEntity = ObjectMapper.Mapper.Map<Adaptadores.Store, Dominio.Entities.Store>(store);
            storesRepository.Update(storeEntity);
        }

        public void DeleteStore(long id)
        {
            IRepository<Dominio.Entities.Store> storesRepository = new InfraestructuraDatos.Repositories.GenericRepository<Dominio.Entities.Store>(szDBContext);
            storesRepository.Delete(id);
        }

        public void SaveChanges()
        {            
            szDBContext.SaveChanges();
        }

    }
}
