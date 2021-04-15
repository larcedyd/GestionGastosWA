namespace CheckIn.API.Models.ModelCedulas
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelCedulas : DbContext
    {
        public ModelCedulas()
            : base("name=ModelCedulas")
        {
        }

        public virtual DbSet<ContribuyentesCR> ContribuyentesCR { get; set; }
        public virtual DbSet<Fisicas> Fisicas { get; set; }
        public virtual DbSet<Sociedades> Sociedades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.TipoIdentificacion)
                .IsUnicode(false);

            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.Apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.CodProvincia)
                .IsUnicode(false);

            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.CodCanton)
                .IsUnicode(false);

            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.CodDistrito)
                .IsUnicode(false);

            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.CodBarrio)
                .IsUnicode(false);

            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ContribuyentesCR>()
                .Property(e => e.CodigoActividadEconomica)
                .IsUnicode(false);

            modelBuilder.Entity<Fisicas>()
                .Property(e => e.Cedula2)
                .IsUnicode(false);

            modelBuilder.Entity<Sociedades>()
                .Property(e => e.Cedula2)
                .IsUnicode(false);
        }
    }
}
