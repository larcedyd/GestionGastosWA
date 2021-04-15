namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DetIngTiendas
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string CodBodega { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumDocto { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short NumLinea { get; set; }

        [Required]
        [StringLength(15)]
        public string CodInterno { get; set; }

        [Required]
        [StringLength(3)]
        public string CodColor { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostoPro { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrecioMayoreo { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrecioVenta { get; set; }

        [StringLength(100)]
        public string Unidades { get; set; }

        public virtual Bodegas Bodegas { get; set; }
    }
}
