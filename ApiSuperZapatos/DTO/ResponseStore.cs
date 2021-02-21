using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.ApiSuperZapatos.DTO
{
    /// <summary>
    /// Clase para devolver respuesta a la petición de un store.
    /// </summary>
    public class ResponseStore : ResponseGeneral
    {
        public Aplicacion.Adaptadores.Store Store { get; set; }
    }
}
