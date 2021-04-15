namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SeguridadModulos
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public SeguridadModulos()
        //{
        //    SeguridadRoles = new HashSet<SeguridadRoles>();
        //}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodModulo { get; set; }

        [Required]
        [StringLength(150)]
        public string Descripcion { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SeguridadRoles> SeguridadRoles { get; set; }
    }
}
