﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.Aplicacion.Adaptadores
{
    /// <summary>
    /// Clase DTO para los stores
    /// </summary>
    public class Store
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
