using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.ApiSuperZapatos.DTO
{
    /// <summary>
    /// Clase para devolver respuesta de error de una petición.
    /// </summary>
    public class ResponseError : ResponseGeneral
    {
        [JsonProperty("error_msg")]        
        public string ErrorMessage { get; set; }
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
    }
}
