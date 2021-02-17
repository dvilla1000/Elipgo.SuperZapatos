using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.ApiSuperZapatos.DTO
{
    public class ResponseError : ResponseGeneral
    {
        public string error_msg { get; set; }
        public int error_code { get; set; }
    }
}
