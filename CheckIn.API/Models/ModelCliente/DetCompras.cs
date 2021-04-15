namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DetCompras
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumCompra { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string CodProveedor { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short NumLinea { get; set; }

        [Required]
        [StringLength(10)]
        public string CodPro { get; set; }

        public decimal Cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal CostoPro { get; set; }

        public decimal? ImptoVta { get; set; }

        public decimal? PorDescuento { get; set; }
    }
}
