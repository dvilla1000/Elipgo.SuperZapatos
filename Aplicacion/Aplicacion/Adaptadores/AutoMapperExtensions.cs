using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elipgo.SuperZapatos.Aplicacion.Adaptadores
{
    /// <summary>
    /// Clase de extensión de funcionalidad del AutoMapper
    /// </summary>
    public static class AutoMapperExtensions
    {
        public static List<TDestination> MapList<TSource, TDestination>(this IMapper mapper, List<TSource> source)
        {
            return mapper.Map<List<TDestination>>(source);
        }
    }

}
