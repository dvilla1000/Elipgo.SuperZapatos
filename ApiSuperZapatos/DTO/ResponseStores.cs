using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.ApiSuperZapatos.DTO
{
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
