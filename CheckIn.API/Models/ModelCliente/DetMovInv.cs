namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetMovInv")]
    public partial class DetMovInv
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumDocto { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string CodMov { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short NumLinea { get; set; }

        [Required]
        [StringLength(10)]
        public string CodPro { get; set; }

        [Column(TypeName = "money")]
        public decimal CostoPro { get; set; }

        public int Cantidad { get; set; }
    }
}
