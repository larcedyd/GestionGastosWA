namespace CheckIn.API.Models.ModelMain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Companias
    {
        [Key]
        [StringLength(6)]
        public string CodCompania { get; set; }

        [StringLength(50)]
        public string NomCompania { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }

        [StringLength(150)]
        public string Contacto { get; set; }

        [StringLength(150)]
        public string BaseDatos { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaRegistro { get; set; }

        [StringLength(50)]
        public string Ubicacion { get; set; }

        public bool? Activo { get; set; }

        [StringLength(20)]
        public string Cedula { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }
    }
}
