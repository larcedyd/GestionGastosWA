namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrdenesExpress")]
    public partial class OrdenesExpress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumFactura { get; set; }

        [StringLength(50)]
        public string NomCliente { get; set; }

        [StringLength(15)]
        public string Telefono { get; set; }

        [Column(TypeName = "text")]
        public string Direccion { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
