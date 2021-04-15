namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetIngMercaderia")]
    public partial class DetIngMercaderia
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string NumDocto { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte NumCaja { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2)]
        public string CodBodega { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short NumLinea { get; set; }

        [Required]
        [StringLength(15)]
        public string CodBarras { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostoPro { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrecioMayoreo { get; set; }

        [StringLength(50)]
        public string CodInterno { get; set; }
    }
}
