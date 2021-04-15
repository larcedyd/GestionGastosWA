namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Colores
    {
        [Key]
        [StringLength(3)]
        public string CodColor { get; set; }

        [StringLength(45)]
        public string NomColor { get; set; }
    }
}
