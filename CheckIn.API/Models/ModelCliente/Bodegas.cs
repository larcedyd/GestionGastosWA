namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bodegas
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Bodegas()
        //{
        //    DetIngTiendas = new HashSet<DetIngTiendas>();
        //    HistInv = new HashSet<HistInv>();
        //}

        [Key]
        [StringLength(2)]
        public string CodBodega { get; set; }

        [Required]
        [StringLength(30)]
        public string NomBodega { get; set; }

        [StringLength(30)]
        public string EncBodega { get; set; }

        [StringLength(15)]
        public string TelBodega { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DetIngTiendas> DetIngTiendas { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<HistInv> HistInv { get; set; }
    }
}
