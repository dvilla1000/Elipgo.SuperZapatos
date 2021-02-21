using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elipgo.SuperZapatos.Aplicacion.Adaptadores
{
    /// <summary>
    /// Clase para configuración del AutoMapper
    /// </summary>
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<CustomMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

}
