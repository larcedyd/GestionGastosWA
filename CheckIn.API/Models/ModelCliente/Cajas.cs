namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cajas
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public Cajas()
        //{
        //    CierreCaja = new HashSet<CierreCaja>();
        //    EncPedidos = new HashSet<EncPedidos>();
        //    EncVtas = new HashSet<EncVtas>();
        //}

        [Key]
        [StringLength(2)]
        public string CodCaja { get; set; }

        [Required]
        [StringLength(20)]
        public string NomCaja { get; set; }

        [StringLength(20)]
        public string SerCpu { get; set; }

        [StringLength(20)]
        public string SerTec { get; set; }

        [StringLength(20)]
        public string SerImp { get; set; }

        [StringLength(20)]
        public string SerMon { get; set; }

        [StringLength(20)]
        public string ModCpu { get; set; }

        [StringLength(20)]
        public string ModTec { get; set; }

        [StringLength(20)]
        public string ModImp { get; set; }

        [StringLength(20)]
        public string ModMon { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CierreCaja> CierreCaja { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EncPedidos> EncPedidos { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EncVtas> EncVtas { get; set; }
    }
}
