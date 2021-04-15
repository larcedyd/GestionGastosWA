namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ActividadesEconomicas
    {
        [Key]
        [StringLength(6)]
        public string CodActividad { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        public bool? Activo { get; set; }
    }
}
