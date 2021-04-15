namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductosPrecios
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodPrecio { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CodPro { get; set; }

        public decimal? Precio { get; set; }

        public virtual Productos Productos { get; set; }
    }
}
