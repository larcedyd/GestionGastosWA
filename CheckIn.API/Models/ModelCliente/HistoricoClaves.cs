using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models.ModelCliente
{
    public class HistoricoClaves
    {
        public int id { get; set; }
        public int idLogin { get; set; }
        public string Clave { get; set; }
        public DateTime Fecha { get; set; }
    }
}