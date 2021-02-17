using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.ApiSuperZapatos.DTO
{
    public class ResponseStores : ResponseGeneral
    {
        public long Total_Elements
        {
            get { return Stores.Count; }
        }
        public IList<Store> Stores { get; set; } //IList<Store>
    }
}
