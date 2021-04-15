namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Abonos
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumApartado { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumFactura { get; set; }

        [Required]
        [StringLength(15)]
        public string CodCliente { get; set; }

        public DateTime FecAbono { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoEfectivo { get; set; }

        [StringLength(2)]
        public string CodBanco { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoOtros { get; set; }

        [StringLength(20)]
        public string NumCheque { get; set; }

        [StringLength(2)]
        public string CodTarjeta { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoTarjeta { get; set; }

        [StringLength(30)]
        public string NumTarjeta { get; set; }

        //public virtual Clientes Clientes { get; set; }
    }
}
