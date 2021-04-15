namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bancos
    {
        [Key]
        [StringLength(2)]
        public string CodBanco { get; set; }

        [Required]
        [StringLength(30)]
        public string NomBanco { get; set; }
    }
}
