namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HistProductos
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public string CodPro { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Mes { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Periodo { get; set; }

        public int UnidCompras { get; set; }

        [Column(TypeName = "money")]
        public decimal MtoCompras { get; set; }

        public int UnidVentas { get; set; }

        [Column(TypeName = "money")]
        public decimal MtoVentas { get; set; }

        [Column(TypeName = "money")]
        public decimal Costo { get; set; }
    }
}
