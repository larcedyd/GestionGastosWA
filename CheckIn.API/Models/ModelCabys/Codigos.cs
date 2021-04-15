namespace CheckIn.API.Models.ModelCabys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Codigos
    {
        [Key]
        [StringLength(50)]
        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public string Impuesto { get; set; }

        public string Categoria1 { get; set; }

        public string DescCategoria1 { get; set; }

        public string Categoria2 { get; set; }

        public string DescCategoria2 { get; set; }

        public string Categoria3 { get; set; }

        public string DescCategoria3 { get; set; }

        public string Categoria4 { get; set; }

        public string DescCategoria4 { get; set; }

        public string Categoria5 { get; set; }

        public string DescCategoria5 { get; set; }

        public string Categoria6 { get; set; }

        public string DescCategoria6 { get; set; }

        public string Categoria7 { get; set; }

        public string DescCategoria7 { get; set; }

        public string Categoria8 { get; set; }

        public string DescCategoria8 { get; set; }
    }
}
