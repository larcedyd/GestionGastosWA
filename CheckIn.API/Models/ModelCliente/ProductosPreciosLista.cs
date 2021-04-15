namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductosPreciosLista")]
    public partial class ProductosPreciosLista
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodPrecio { get; set; }

        [StringLength(50)]
        public string NomLista { get; set; }
    }
}
