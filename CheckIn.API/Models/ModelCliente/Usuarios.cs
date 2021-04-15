namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuarios
    {
        [StringLength(2)]
        public string CodUsuario { get; set; }

        [StringLength(30)]
        public string NomUsuario { get; set; }

        [StringLength(50)]
        public string ClavePaso { get; set; }

        [StringLength(100)]
        public string Accesos { get; set; }

        [Key]
        public bool AnulaFactura { get; set; }
    }
}
