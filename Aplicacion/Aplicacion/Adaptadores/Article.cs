﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.Aplicacion.Adaptadores
{
    /// <summary>
    /// Clase DTO para los articulos
    /// </summary>
    public class Article
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double TotalInShelf { get; set; }
        public double TotalInVault { get; set; }
        public long StoreId { get; set; }
    }
}
