using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.Aplicacion.Adaptadores
{
    public class Article
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double TotalInShelf { get; set; }
        public double TotalInVault { get; set; }
    }
}
