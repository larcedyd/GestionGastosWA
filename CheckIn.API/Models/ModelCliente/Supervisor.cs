namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supervisor")]
    public partial class Supervisor
    {
        [Key]
        [StringLength(2)]
        public string CodSupervisor { get; set; }

        [Required]
        [StringLength(30)]
        public string NomSupervisor { get; set; }

        [Required]
        [StringLength(15)]
        public string ClavePaso { get; set; }
    }
}
