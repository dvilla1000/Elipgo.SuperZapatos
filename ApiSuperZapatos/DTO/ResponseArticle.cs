using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.ApiSuperZapatos.DTO
{
    /// <summary>
    /// Clase para devolver respuesta a la petición de un articulo.
    /// </summary>
    public class ResponseArticle : ResponseGeneral
    {
        public Aplicacion.Adaptadores.Article Article { get; set; }
    }
}
