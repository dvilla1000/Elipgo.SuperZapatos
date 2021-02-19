using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elipgo.SuperZapatos.Aplicacion.Adaptadores
{
    public static class AutoMapperExtensions
    {
        public static List<TDestination> MapList<TSource, TDestination>(this IMapper mapper, List<TSource> source)
        {
            return mapper.Map<List<TDestination>>(source);
        }
    }

}
