﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models
{
    public class Filtros
    {
        public string Texto { get; set; }
        public int Codigo1 { get; set; }
        public int Codigo2 { get; set; }
        public int Codigo3 { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Asignados { get; set; }
        public string Estado { get; set; }

    }
}