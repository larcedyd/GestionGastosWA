namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Corridas
    {
        [Key]
        [StringLength(2)]
        public string CodCorrida { get; set; }

        [StringLength(30)]
        public string NomCorrida { get; set; }

        [StringLength(100)]
        public string Tallas { get; set; }
    }
}
