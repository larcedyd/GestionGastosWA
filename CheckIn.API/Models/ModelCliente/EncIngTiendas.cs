namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EncIngTiendas
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string CodBodega { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumDocto { get; set; }

        public DateTime FecDocto { get; set; }

        [Required]
        [StringLength(20)]
        public string NumFactura { get; set; }

        [Required]
        [StringLength(3)]
        public string CodProveedor { get; set; }

        public double? TotalUnidades { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalDocto { get; set; }

        public bool Aplicado { get; set; }

        public virtual Proveedores Proveedores { get; set; }
    }
}
