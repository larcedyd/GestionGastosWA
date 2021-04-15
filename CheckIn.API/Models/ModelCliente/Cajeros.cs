namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cajeros
    {
        [Key]
        [StringLength(2)]
        public string CodCajero { get; set; }

        [Required]
        [StringLength(50)]
        public string NomCajero { get; set; }

        [Required]
        [StringLength(50)]
        public string ClavePaso { get; set; }
    }
}
