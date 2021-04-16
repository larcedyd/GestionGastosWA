namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EncCompras
    {
        [Required]
        [StringLength(20)]
        public string CodEmpresa { get; set; }

        [Required]
        [StringLength(20)]
        public string CodProveedor { get; set; }

        [Required]
        [StringLength(10)]
        public string TipoDocumento { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumFactura { get; set; }

        public DateTime? FecFactura { get; set; }

        [StringLength(10)]
        public string TipoIdentificacionCliente { get; set; }

        [StringLength(20)]
        public string CodCliente { get; set; }

        [StringLength(500)]
        public string NomCliente { get; set; }

        [StringLength(500)]
        public string EmailCliente { get; set; }

        public int? DiasCredito { get; set; }

        [StringLength(2)]
        public string CondicionVenta { get; set; }

        [StringLength(50)]
        public string ClaveHacienda { get; set; }

        [StringLength(20)]
        public string ConsecutivoHacienda { get; set; }

        [StringLength(2)]
        public string MedioPago { get; set; }

        public byte? Situacion { get; set; }

        [StringLength(3)]
        public string CodMoneda { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalServGravados { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalServExentos { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalMercanciasGravadas { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalMercanciasExentas { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalExento { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalVenta { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalDescuentos { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalVentaNeta { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalImpuesto { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalComprobante { get; set; }

        public string XmlFacturaRecibida { get; set; }

        [StringLength(1)]
        public string Estado { get; set; }

        [StringLength(500)]
        public string Observacion { get; set; }

        public DateTime? FechaGravado { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalServExonerado { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalMercExonerada { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalExonerado { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalIVADevuelto { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalOtrosCargos { get; set; }

        [StringLength(6)]
        public string CodigoActividadEconomica { get; set; }

        public int? idLoginAsignado { get; set; }

        public DateTime? FecAsignado { get; set; }

        public int? idLoginAceptacion { get; set; }

        public string PdfFactura { get; set; }

        public int? idNormaReparto { get; set; }

        public int? idTipoGasto { get; set; }

        public int? idCierre { get; set; }
    }
}
