namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Parqueo")]
    public partial class Parqueo
    {
        [Key]
        public int Codigo { get; set; }

        [StringLength(15)]
        public string NumPlaca { get; set; }

        public DateTime? FechaEntrada { get; set; }

        public DateTime? FechaSalida { get; set; }

        [StringLength(50)]
        public string Usuario { get; set; }

        public int? NumFactura { get; set; }

        [Column(TypeName = "money")]
        public decimal? MontoCobrado { get; set; }

        [StringLength(2)]
        public string TipoVehiculo { get; set; }
    }
}
