namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Precios
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string CodPro { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string CodColor { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string CodTalla { get; set; }

        [StringLength(12)]
        public string CodBarras { get; set; }

        public DateTime? FecIni { get; set; }

        public DateTime? FecFin { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrecioVenta { get; set; }

        public virtual Tallas Tallas { get; set; }
    }
}
