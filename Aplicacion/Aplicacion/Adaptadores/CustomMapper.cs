using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elipgo.SuperZapatos.Aplicacion.Adaptadores
{
    /// <summary>
    /// Clase para establecer el mapeo de las clases concretas
    /// </summary>
    public class CustomMapper : Profile
    {
        public CustomMapper()
        {
            CreateMap<Dominio.Entities.Store, Adaptadores.Store>();
            CreateMap<Dominio.Entities.Article, Adaptadores.Article>();

            CreateMap<Adaptadores.Store, Dominio.Entities.Store> ();
            CreateMap<Adaptadores.Article, Dominio.Entities.Article>();
        }
    }
}
