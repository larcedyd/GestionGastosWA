namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NotasCredito")]
    public partial class NotasCredito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumDocto { get; set; }

        public DateTime? FechaDocto { get; set; }

        [Column(TypeName = "money")]
        public decimal? Monto { get; set; }

        [Column(TypeName = "money")]
        public decimal? Abono { get; set; }

        public int? NumFactura { get; set; }

        [StringLength(50)]
        public string NomCliente { get; set; }

        [StringLength(15)]
        public string CodCliente { get; set; }
    }
}
