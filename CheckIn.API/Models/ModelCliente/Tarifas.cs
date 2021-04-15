namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tarifas
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodTarifa { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string Tipo { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2)]
        public string TipoVehiculo { get; set; }

        [Column(TypeName = "money")]
        public decimal? Precio { get; set; }

        public int? CantMinutos { get; set; }
    }
}
