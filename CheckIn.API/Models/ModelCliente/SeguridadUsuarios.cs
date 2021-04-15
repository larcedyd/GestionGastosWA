namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SeguridadUsuarios
    {

        [StringLength(3)]
        public string CodCompania { get; set; }

        [Key]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string NomUsuario { get; set; }

        [StringLength(100)]
        public string Clave { get; set; }

        [StringLength(1)]
        public string Activo { get; set; }

        public int? Codrol { get; set; }

       // public virtual SeguridadRoles SeguridadRoles { get; set; }
    }
}
