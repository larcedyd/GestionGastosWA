namespace CheckIn.API.Models.ModelMain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelMain : DbContext
    {
        public ModelMain()
            : base("name=ModelMain")
        {
        }

        public virtual DbSet<Cajeros> Cajeros { get; set; }
        public virtual DbSet<Companias> Companias { get; set; }
        public virtual DbSet<LECTURAMENSAJESPANTALLA> LECTURAMENSAJESPANTALLA { get; set; }
        public virtual DbSet<MensajesPantalla> MensajesPantalla { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cajeros>()
                .Property(e => e.CodCompania)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Cajeros>()
                .Property(e => e.CodCajero)
                .IsUnicode(false);

            modelBuilder.Entity<Cajeros>()
                .Property(e => e.NomCajero)
                .IsUnicode(false);

            modelBuilder.Entity<Cajeros>()
                .Property(e => e.Clave)
                .IsUnicode(false);

            modelBuilder.Entity<Companias>()
                .Property(e => e.CodCompania)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Companias>()
                .Property(e => e.NomCompania)
                .IsUnicode(false);

            modelBuilder.Entity<Companias>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Companias>()
                .Property(e => e.Contacto)
                .IsUnicode(false);

            modelBuilder.Entity<Companias>()
                .Property(e => e.BaseDatos)
                .IsUnicode(false);

            modelBuilder.Entity<Companias>()
                .Property(e => e.Ubicacion)
                .IsUnicode(false);

            modelBuilder.Entity<Companias>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Companias>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<MensajesPantalla>()
                .Property(e => e.Mensaje)
                .IsUnicode(false);

            modelBuilder.Entity<MensajesPantalla>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);
        }
    }
}
