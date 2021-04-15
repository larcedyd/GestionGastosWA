namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TiposMovimientos
    {
        [Key]
        [StringLength(2)]
        public string CodMov { get; set; }

        [Required]
        [StringLength(30)]
        public string NomMov { get; set; }

        public float Conversion { get; set; }

        public int NumDoctoSig { get; set; }

        public bool Modifica { get; set; }
    }
}
