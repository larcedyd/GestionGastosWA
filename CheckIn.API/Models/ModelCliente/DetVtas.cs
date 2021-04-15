namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DetVtas
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumFactura { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short NumLinea { get; set; }

        [Required]
        [StringLength(15)]
        public string CodPro { get; set; }

        [Column(TypeName = "money")]
        public decimal PrecioVenta { get; set; }

        public decimal Cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal CostoPro { get; set; }

        public float? PorDescto { get; set; }

        [StringLength(50)]
        public string NomPro { get; set; }

        public decimal? ImptoVta { get; set; }

        [StringLength(20)]
        public string CodProHacienda { get; set; }

        //public virtual EncVtas EncVtas { get; set; }
    }
}
