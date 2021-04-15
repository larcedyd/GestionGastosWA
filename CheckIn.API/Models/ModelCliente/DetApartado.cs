namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetApartado")]
    public partial class DetApartado
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumApartado { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short NumLinea { get; set; }

        [Required]
        [StringLength(10)]
        public string CodPro { get; set; }

        public short Cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal PrecioVenta { get; set; }

        [Column(TypeName = "money")]
        public decimal? PorDescto { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostoPro { get; set; }

        [StringLength(20)]
        public string CodProHacienda { get; set; }

       // public virtual EncApartado EncApartado { get; set; }
    }
}
