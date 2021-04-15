namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HistInv")]
    public partial class HistInv
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string CodMov { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string NumDocto { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(15)]
        public string CodPro { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumLinea { get; set; }

        [StringLength(2)]
        public string CodBodega { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal Costo { get; set; }

        [StringLength(3)]
        public string CodProveedor { get; set; }

        public virtual Bodegas Bodegas { get; set; }
    }
}
