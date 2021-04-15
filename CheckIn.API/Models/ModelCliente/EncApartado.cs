namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EncApartado")]
    public partial class EncApartado
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public EncApartado()
        //{
        //    DetApartado = new HashSet<DetApartado>();
        //}

        [Key]
        public int NumApartado { get; set; }

        [Required]
        [StringLength(15)]
        public string CodCliente { get; set; }

        public DateTime FecApartado { get; set; }

        public DateTime FecVence { get; set; }

        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        public decimal? ImptoVentas { get; set; }

        [Column(TypeName = "money")]
        public decimal? Descuento { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [Column(TypeName = "money")]
        public decimal AbonoCompra { get; set; }

        [StringLength(2)]
        public string CodVendedor { get; set; }

        public virtual Clientes Clientes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DetApartado> DetApartado { get; set; }
    }
}
