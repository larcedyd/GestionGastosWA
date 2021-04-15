namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lineas
    {
        [Key]
        [StringLength(3)]
        public string CodLinea { get; set; }

        [StringLength(30)]
        public string NomLinea { get; set; }
    }
}
