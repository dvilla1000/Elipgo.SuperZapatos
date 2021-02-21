using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.ApiSuperZapatos.DTO
{
    /// <summary>
    /// Clase para devolver respuesta a la petición de una colección de stores.
    /// </summary>
    public class ResponseStores : ResponseGeneral
    {
        [JsonProperty("total_elements")]
        public long TotalElements
        {
            get { return Stores.Count(); }
        }
        
        public IList<Aplicacion.Adaptadores.Store> Stores { get; set; }
    }
}
