namespace CheckIn.API.Models.ModelCedulas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContribuyentesCR")]
    public partial class ContribuyentesCR
    {
        [Key]
        [StringLength(20)]
        public string Cedula { get; set; }

        [StringLength(2)]
        public string TipoIdentificacion { get; set; }

        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Apellidos { get; set; }

        [StringLength(1)]
        public string CodProvincia { get; set; }

        [StringLength(2)]
        public string CodCanton { get; set; }

        [StringLength(2)]
        public string CodDistrito { get; set; }

        [StringLength(2)]
        public string CodBarrio { get; set; }

        [StringLength(500)]
        public string Direccion { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(10)]
        public string CodigoActividadEconomica { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
