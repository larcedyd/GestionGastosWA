namespace CheckIn.API.Models.ModelCabys
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelCabys : DbContext
    {
        public ModelCabys()
            : base("name=ModelCabys")
        {
        }

        public virtual DbSet<Codigos> Codigos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Codigos>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.Impuesto)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.Categoria1)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.DescCategoria1)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.Categoria2)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.DescCategoria2)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.Categoria3)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.DescCategoria3)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.Categoria4)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.DescCategoria4)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.Categoria5)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.DescCategoria5)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.Categoria6)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.DescCategoria6)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.Categoria7)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.DescCategoria7)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.Categoria8)
                .IsUnicode(false);

            modelBuilder.Entity<Codigos>()
                .Property(e => e.DescCategoria8)
                .IsUnicode(false);
        }
    }
}
