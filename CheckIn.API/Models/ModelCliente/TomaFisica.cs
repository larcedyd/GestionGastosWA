namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TomaFisica")]
    public partial class TomaFisica
    {
        [StringLength(12)]
        public string CodBarras { get; set; }

        [Key]
        [StringLength(10)]
        public string NombreSeccion { get; set; }

        public short? ConteoFisico { get; set; }
    }
}
