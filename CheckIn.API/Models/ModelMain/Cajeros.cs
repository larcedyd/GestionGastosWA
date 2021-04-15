namespace CheckIn.API.Models.ModelMain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cajeros
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string CodCompania { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string CodCajero { get; set; }

        [Required]
        [StringLength(50)]
        public string NomCajero { get; set; }

        [Required]
        [StringLength(50)]
        public string Clave { get; set; }
    }
}
