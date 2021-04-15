namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CodigosTarifaImpuestos
    {
        [Key]
        [StringLength(2)]
        public string CodTarifa { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        public bool? Activo { get; set; }
    }
}
