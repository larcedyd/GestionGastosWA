namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bitacora")]
    public partial class Bitacora
    {
        [Key]
        public int CodBitacora { get; set; }

        [Required]
        public string Suceso { get; set; }

        [Required]
        [StringLength(50)]
        public string Usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Computer { get; set; }

        [StringLength(250)]
        public string Sistema { get; set; }

        [StringLength(150)]
        public string UsuarioDB { get; set; }

        [StringLength(50)]
        public string Accion { get; set; }

        public DateTime? FechaCreacion { get; set; }
    }
}
