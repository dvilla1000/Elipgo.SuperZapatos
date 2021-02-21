using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.ApiSuperZapatos.DTO
{
    /// <summary>
    /// Clase para devolver respuesta a la petición de una colección de articulos.
    /// </summary>
    public class ResponseArticles
        : ResponseGeneral
    {
        [JsonProperty("total_elements")]
        public long TotalElements
        {
            get { return Articles.Count; }
        }

        public IList<Aplicacion.Adaptadores.Article> Articles { get; set; }

    }
}
