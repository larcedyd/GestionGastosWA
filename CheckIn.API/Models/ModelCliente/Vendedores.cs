namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vendedores
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Vendedores()
        //{
        //    EncPedidos = new HashSet<EncPedidos>();
        //    EncVtas = new HashSet<EncVtas>();
        //    HistVentas = new HashSet<HistVentas>();
        //}

        [Key]
        [StringLength(2)]
        public string CodVendedor { get; set; }

        [Required]
        [StringLength(30)]
        public string NomVendedor { get; set; }

        public float PorComision { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EncPedidos> EncPedidos { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EncVtas> EncVtas { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<HistVentas> HistVentas { get; set; }
    }
}
