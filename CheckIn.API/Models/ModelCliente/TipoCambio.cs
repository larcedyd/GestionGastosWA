namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoCambio")]
    public partial class TipoCambio
    {
        [Key]
        public DateTime Fecha { get; set; }

        [Column("TipoCambio", TypeName = "money")]
        public decimal? TipoCambio1 { get; set; }
    }
}
