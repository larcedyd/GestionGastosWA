namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CierreCaja")]
    public partial class CierreCaja
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string CodCajero { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string CodCaja { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime FecCaja { get; set; }

        public DateTime FechaSistema { get; set; }

        [StringLength(15)]
        public string CodSupervisor { get; set; }

        [Column(TypeName = "money")]
        public decimal? Efectivo { get; set; }

        [Column(TypeName = "money")]
        public decimal? Cheques { get; set; }

        [Column(TypeName = "money")]
        public decimal? Tarjetas { get; set; }

        [Column(TypeName = "money")]
        public decimal? OtrosPagos { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoVendido { get; set; }

        [Column(TypeName = "money")]
        public decimal? Diferencia { get; set; }

        public bool Abierta { get; set; }

        public DateTime? HoraCierre { get; set; }

        [Column(TypeName = "money")]
        public decimal? MontoApertura { get; set; }

        [Column(TypeName = "money")]
        public decimal? CortesEfectivo { get; set; }

        [StringLength(250)]
        public string CortesDescripcion { get; set; }

        public double? NumCierre { get; set; }

        public bool? Activa { get; set; }

        [Column(TypeName = "money")]
        public decimal? Dolares { get; set; }

        [Column(TypeName = "money")]
        public decimal? TipoCambio { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        //public virtual Cajas Cajas { get; set; }
    }
}
