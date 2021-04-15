namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tarjetas
    {
        [Key]
        [StringLength(2)]
        public string CodTarjeta { get; set; }

        [Required]
        [StringLength(30)]
        public string NomTarjeta { get; set; }

        [StringLength(30)]
        public string EnteEmisor { get; set; }

        public float? PorComision { get; set; }
    }
}
