namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EncCompras
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumCompra { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string CodProveedor { get; set; }

        public DateTime FechaCompra { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        [Column(TypeName = "money")]
        public decimal? SubTotal { get; set; }

        [Column(TypeName = "money")]
        public decimal? ImptoVta { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalDescuento { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalCompra { get; set; }
    }
}
