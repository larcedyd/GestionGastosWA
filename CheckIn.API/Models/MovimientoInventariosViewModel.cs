using CheckIn.API.Models.ModelCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models
{
    public class MovimientoInventariosViewModel
    {
        public EncMovInv EncMovInv { get; set; }
        public DetMovInv[] detMovInv { get; set; }
        public  HistInv[] HistoInv { get; set; }
    }
}