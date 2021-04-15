using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models
{
    public class Filtros
    {
        public string Codigo { get; set; }
        public int Cod { get; set; }
        public string Texto { get; set; }
        public int NumFactura { get; set; }
        public bool Cotizaciones { get; set; } = false;
        public string CodBarras { get; set; }
        public bool Anulada { get; set; } = false;
        public string CodProveedor { get; set; }
        public string LineaInicial { get; set; }
        public string LineaFinal { get; set; }
        public bool Existencia { get; set; } = false;
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Vencidos { get; set; } = false;
        public bool SCancelar { get; set; } = false;
        public bool Detallado { get; set; } = false;
        public int FacturaInicial { get; set; } = 0;
        public int FacturaFinal { get; set; } = 0;

    }
}