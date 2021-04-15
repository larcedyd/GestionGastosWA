namespace CheckIn.API.Models.ModelMain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LECTURAMENSAJESPANTALLA")]
    public partial class LECTURAMENSAJESPANTALLA
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public DateTime? FECHALECTURA { get; set; }

        public bool? NOMOSTRARMAS { get; set; }
    }
}
