using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CheckIn.API.Models.ModelCliente
{

    [Table("LogsUsuarios")]
    public class LogsUsuarios
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
    }
}