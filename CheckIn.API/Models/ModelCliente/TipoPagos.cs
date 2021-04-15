namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TipoPagos
    {
        [Key]
        [StringLength(2)]
        public string CodPago { get; set; }

        [StringLength(30)]
        public string NomPago { get; set; }
    }
}
