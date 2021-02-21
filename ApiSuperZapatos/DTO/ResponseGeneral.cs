using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.ApiSuperZapatos.DTO
{
    /// <summary>
    /// Clase base para las respuestas de peticiones
    /// </summary>
    public abstract class ResponseGeneral
    {
        public bool Success { get; set; }

    }
}
