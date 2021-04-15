namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ZVentasxVendedor")]
    public partial class ZVentasxVendedor
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string CodLinea { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string CodVendedor { get; set; }

        public int? UnidadesVendidas { get; set; }

        [Column(TypeName = "money")]
        public decimal? Precio { get; set; }
    }
}
