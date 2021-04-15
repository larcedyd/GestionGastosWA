namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HistVentas
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string CodVendedor { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Mes { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Año { get; set; }

        public int UnidVentas { get; set; }

        [Column(TypeName = "money")]
        public decimal MtoVentas { get; set; }

        [Column(TypeName = "money")]
        public decimal CostoPro { get; set; }

        public virtual Vendedores Vendedores { get; set; }
    }
}
