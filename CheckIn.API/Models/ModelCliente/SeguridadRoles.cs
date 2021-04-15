namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SeguridadRoles
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public SeguridadRoles()
        //{
        //    SeguridadUsuarios = new HashSet<SeguridadUsuarios>();
        //    SeguridadModulos = new HashSet<SeguridadModulos>();
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodRol { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SeguridadUsuarios> SeguridadUsuarios { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SeguridadModulos> SeguridadModulos { get; set; }
    }
}
