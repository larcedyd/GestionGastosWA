using CheckIn.API.Models.ModelCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models
{
    public class VentasViewModel
    {
        public Clientes Cliente { get; set; }
        public EncVtas EncVtas { get; set; }
        public DetVtas[] DetVtas { get; set; }
        public OrdenesExpress OrdenesExpress { get; set; }
    }
}