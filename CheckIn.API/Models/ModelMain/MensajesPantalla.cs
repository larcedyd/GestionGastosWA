namespace CheckIn.API.Models.ModelMain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MensajesPantalla")]
    public partial class MensajesPantalla
    {
        public int Id { get; set; }

        public string Mensaje { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public bool? Activo { get; set; }

        public DateTime? FechaExpiracion { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }
    }
}
