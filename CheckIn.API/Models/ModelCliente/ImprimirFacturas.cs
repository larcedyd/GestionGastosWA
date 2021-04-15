namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImprimirFacturas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumFactura { get; set; }

        [StringLength(2)]
        public string CodCaja { get; set; }

        public bool? Impresa { get; set; }
    }
}
