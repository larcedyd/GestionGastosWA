namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EncVtas
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public EncVtas()
        //{
        //    DetVtas = new HashSet<DetVtas>();
        //}

        [Key]
        public int NumFactura { get; set; }

        [Required]
        [StringLength(15)]
        public string CodCliente { get; set; }

        [StringLength(100)]
        public string NomCliente { get; set; }

        public DateTime? FecFactura { get; set; }

        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "money")]
        public decimal Descuento { get; set; }

        [Column(TypeName = "money")]
        public decimal ImptoVentas { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public int? UnidadesVendidas { get; set; }

        [Required]
        [StringLength(2)]
        public string CodVendedor { get; set; }

        [Required]
        [StringLength(2)]
        public string CodCajero { get; set; }

        [Column(TypeName = "money")]
        public decimal MtoEfectivo { get; set; }

        [StringLength(3)]
        public string CodBanco { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoCheque { get; set; }

        [StringLength(20)]
        public string NumCheque { get; set; }

        [StringLength(2)]
        public string CodTarjeta { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoTarjeta { get; set; }

        [StringLength(30)]
        public string NumTarjeta { get; set; }

        [StringLength(2)]
        public string CodPago { get; set; }

        [StringLength(15)]
        public string NumPago { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoPago { get; set; }

        [StringLength(2)]
        public string CodCaja { get; set; }

        public bool Anulada { get; set; }

        public bool Apdo { get; set; }

        public double? NumCierre { get; set; }

        public int? NumApartado { get; set; }

        [StringLength(100)]
        public string MotivoAnulacion { get; set; }

        [StringLength(15)]
        public string CodSupervisor { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoDol { get; set; }

       


        public DateTime? FechaRegistro { get; set; }

        public bool? Impreso { get; set; }

        [Column(TypeName = "money")]
        public decimal? CambioAbono { get; set; }

        [StringLength(50)]
        public string FAClave { get; set; }

        public bool? FAProcesado { get; set; }

        [StringLength(50)]
        public string FAConsecutivo { get; set; }

        [Column(TypeName = "text")]
        public string FAError { get; set; }

        [StringLength(250)]
        public string FAResolucion { get; set; }

        public bool? FAContingencia { get; set; }

        [Column(TypeName = "money")]
        public decimal? TipoCambio { get; set; }

        [StringLength(50)]
        public string FAClaveHacienda { get; set; }

        [StringLength(50)]
        public string FANotaCreditoConsecutivo { get; set; }

        [StringLength(50)]
        public string FANotaCreditoClaveHacienda { get; set; }

        public bool? FAContingenciaLocal { get; set; }

        [Column(TypeName = "text")]
        public string FAJson { get; set; }

        public decimal? TotalApartado { get; set; }

        [StringLength(20)]
        public string Cedula { get; set; }

        public int? CondicionVenta { get; set; }

        public int? DiasCredito { get; set; }

        public int? NumFacturaGaxpar { get; set; }

        public int? FEIntentos { get; set; }

        public decimal? Impuesto1 { get; set; }

        public decimal? Impuesto2 { get; set; }

        public decimal? Impuesto4 { get; set; }

        public decimal? Impuesto8 { get; set; }

        public decimal? Impuesto13 { get; set; }

        public decimal? TotalServiciosExentos { get; set; }

        public decimal? TotalServiciosGravados { get; set; }

        public decimal? TotalMercaderiasExentas { get; set; }

        public decimal? TotalMercaderiasGravadas { get; set; }

        public bool? AbonoApartado { get; set; }

        //public virtual Cajas Cajas { get; set; }

        //public virtual Clientes Clientes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DetVtas> DetVtas { get; set; }

        //public virtual Vendedores Vendedores { get; set; }
    }
}
