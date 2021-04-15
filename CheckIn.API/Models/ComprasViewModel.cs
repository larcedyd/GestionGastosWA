using CheckIn.API.Models.ModelCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models
{
    public class ComprasViewModel
    {
        public EncCompras EncCompras { get; set; }
        public DetCompras[] DetCompras { get; set; }
    }
}