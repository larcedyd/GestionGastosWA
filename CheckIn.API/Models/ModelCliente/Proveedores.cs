namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Proveedores
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Proveedores()
        //{
        //    EncIngTiendas = new HashSet<EncIngTiendas>();
        //    Productos = new HashSet<Productos>();
        //}

        [Key]
        [StringLength(3)]
        public string CodProveedor { get; set; }

        [Required]
        [StringLength(30)]
        public string NomProveedor { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Encargado { get; set; }

        [StringLength(15)]
        public string Telefono { get; set; }

        [StringLength(15)]
        public string Fax { get; set; }

        [StringLength(15)]
        public string Celular { get; set; }

        [Column(TypeName = "money")]
        public decimal? MtoCompras { get; set; }

        public DateTime? FecUltCompra { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EncIngTiendas> EncIngTiendas { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Productos> Productos { get; set; }
    }
}
