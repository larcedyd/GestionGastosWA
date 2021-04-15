namespace CheckIn.API.Models.ModelCedulas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sociedades
    {
        [Key]
        [StringLength(12)]
        public string CEDULA { get; set; }

        [StringLength(80)]
        public string NOMBRE { get; set; }

        [StringLength(12)]
        public string Cedula2 { get; set; }
    }
}
