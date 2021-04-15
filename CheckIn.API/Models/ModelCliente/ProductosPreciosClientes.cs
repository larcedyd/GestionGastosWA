namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductosPreciosClientes
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string CodPro { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public string CodCliente { get; set; }

        public decimal? Precio { get; set; }
    }
}
