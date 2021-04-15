namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Parametros
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string NumTienda { get; set; }

        [Key]
        [Column(Order = 1)]
        public float ImptoVta { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string NombreTienda { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string CedulaJuridica { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(250)]
        public string Ubicacion { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime FechaSistema { get; set; }

        [StringLength(2)]
        public string BodegaActual { get; set; }

        [StringLength(15)]
        public string Telefono { get; set; }

        public double? NumCierre { get; set; }

        [StringLength(25)]
        public string Denominacion { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool FacturaExistenciasNegativas { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool ImprimeCambioCero { get; set; }

        public double? NumeroFacturaNula { get; set; }

        public double? NumApdoNulo { get; set; }

        public double? NumFacturaNula { get; set; }

        [StringLength(60)]
        public string UbicacionAccess { get; set; }

        public byte? TipoFacturaCliente { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool ActivaPrecioxMayor { get; set; }

        [StringLength(50)]
        public string EmailDe { get; set; }

        [StringLength(5)]
        public string EmailPuerto { get; set; }

        [StringLength(50)]
        public string EmailUser { get; set; }

        [StringLength(50)]
        public string EmailPassword { get; set; }

        [StringLength(50)]
        public string EmailServer { get; set; }

        public bool? EmailEnableSSL { get; set; }

        [StringLength(50)]
        public string IP_WebSite { get; set; }

        public short? MaximoDescuento { get; set; }

        public short? ImpresionCantFacturas { get; set; }

        public short? ImpresionCantApartados { get; set; }

        public short? ImpresionCantAbonos { get; set; }

        [StringLength(550)]
        public string UrlAdministrativo { get; set; }

        public bool? ActivarFacturaElectronica { get; set; }

        [StringLength(50)]
        public string FACodEmpresa { get; set; }

        [StringLength(50)]
        public string FACodSucursal { get; set; }

        [StringLength(50)]
        public string FANIT { get; set; }

        public bool? Parqueo { get; set; }

        [StringLength(500)]
        public string UrlWebApi { get; set; }

        [StringLength(2)]
        public string TipoIdentificacion { get; set; }

        [StringLength(550)]
        public string ResolucionHacienda { get; set; }

        [StringLength(15)]
        public string CodClienteDefault { get; set; }

        [StringLength(2)]
        public string CodVendedorDefault { get; set; }

        public int? CantidadLineasXPagina { get; set; }

        public bool? MostrarImprimirFactura { get; set; }

        [StringLength(6)]
        public string CodigoActividadEconomica { get; set; }

        public bool? ActivaProductosLineaNueva { get; set; }

        public bool? CodBarrasPeso { get; set; }

        public bool? ActivaOrdenExpress { get; set; }

        public bool? ActivaApartados { get; set; }

        public bool? ActivaPedidos { get; set; }

        public bool? ActivaDiasCredito { get; set; }

        [StringLength(200)]
        public string ConexionGaxpar { get; set; }

        [StringLength(10)]
        public string CodEmpresaGaxpar { get; set; }

        public int? CodClienteGaxpar { get; set; }

        public bool? ActivaMultiPrecios { get; set; }

        [StringLength(5)]
        public string PrefijoRomanas { get; set; }

        [StringLength(50)]
        public string VersionTicket { get; set; }
    }
}
