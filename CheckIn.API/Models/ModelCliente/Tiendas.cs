namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tiendas
    {
        [Key]
        [StringLength(2)]
        public string CodTienda { get; set; }

        [StringLength(30)]
        public string NomTienda { get; set; }

        [StringLength(50)]
        public string UbicacionBaseDatos { get; set; }
    }
}
