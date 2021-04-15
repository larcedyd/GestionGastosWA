namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EncMovInv")]
    public partial class EncMovInv
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NumDocto { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string CodMov { get; set; }

        [StringLength(50)]
        public string Detalle { get; set; }

        public DateTime FecDocto { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalMov { get; set; }
    }
}
