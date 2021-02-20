using System;
using System.Collections.Generic;
using System.Text;

namespace Elipgo.SuperZapatos.AppFormSuperZapatos.Models
{
    public class StoresModel
    {
        public bool Success { get; set; }
        public long TotalElements { get; set; }
        public IList<Store> Stores { get; set; }
    }

    public class Store {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

    }
}
