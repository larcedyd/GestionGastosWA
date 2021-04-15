namespace CheckIn.API.Models.ModelCliente
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelCliente : DbContext
    {
        public ModelCliente(string connectionString, bool lazyLoadinEnabled = true)
            : base("name=ModelCliente")
        {
            this.Database.Connection.ConnectionString = connectionString;
            try
            {
                this.Database.Connection.Open();
                this.Configuration.LazyLoadingEnabled = lazyLoadinEnabled;
            }
            catch { }
        }

        public virtual DbSet<Abonos> Abonos { get; set; }
        public virtual DbSet<ActividadesEconomicas> ActividadesEconomicas { get; set; }
        public virtual DbSet<Bancos> Bancos { get; set; }
        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<Bodegas> Bodegas { get; set; }
        public virtual DbSet<Cajas> Cajas { get; set; }
        public virtual DbSet<Cajeros> Cajeros { get; set; }
        public virtual DbSet<CierreCaja> CierreCaja { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<CodigosTarifaImpuestos> CodigosTarifaImpuestos { get; set; }
        public virtual DbSet<Colores> Colores { get; set; }
        public virtual DbSet<Corridas> Corridas { get; set; }
        public virtual DbSet<Departamentos> Departamentos { get; set; }
        public virtual DbSet<DetApartado> DetApartado { get; set; }
        public virtual DbSet<DetCompras> DetCompras { get; set; }
        public virtual DbSet<DetIngMercaderia> DetIngMercaderia { get; set; }
        public virtual DbSet<DetIngTiendas> DetIngTiendas { get; set; }
        public virtual DbSet<DetMovInv> DetMovInv { get; set; }
        public virtual DbSet<DetPedidos> DetPedidos { get; set; }
        public virtual DbSet<DetVtas> DetVtas { get; set; }
        public virtual DbSet<EncApartado> EncApartado { get; set; }
        public virtual DbSet<EncCompras> EncCompras { get; set; }
        public virtual DbSet<EncIngTiendas> EncIngTiendas { get; set; }
        public virtual DbSet<EncMovInv> EncMovInv { get; set; }
        public virtual DbSet<EncPedidos> EncPedidos { get; set; }
        public virtual DbSet<EncVtas> EncVtas { get; set; }
        public virtual DbSet<HistInv> HistInv { get; set; }
        public virtual DbSet<HistProductos> HistProductos { get; set; }
        public virtual DbSet<HistVentas> HistVentas { get; set; }
        public virtual DbSet<ImprimirFacturas> ImprimirFacturas { get; set; }
        public virtual DbSet<LECTURAMENSAJESPANTALLA> LECTURAMENSAJESPANTALLA { get; set; }
        public virtual DbSet<Lineas> Lineas { get; set; }
        public virtual DbSet<Marcas> Marcas { get; set; }
        public virtual DbSet<NotasCredito> NotasCredito { get; set; }
        public virtual DbSet<OrdenesExpress> OrdenesExpress { get; set; }
        public virtual DbSet<Parqueo> Parqueo { get; set; }
        public virtual DbSet<Precios> Precios { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<ProductosPrecios> ProductosPrecios { get; set; }
        public virtual DbSet<ProductosPreciosClientes> ProductosPreciosClientes { get; set; }
        public virtual DbSet<ProductosPreciosLista> ProductosPreciosLista { get; set; }
        public virtual DbSet<Proveedores> Proveedores { get; set; }
        public virtual DbSet<SeguridadModulos> SeguridadModulos { get; set; }
        public virtual DbSet<SeguridadRoles> SeguridadRoles { get; set; }
        public virtual DbSet<SeguridadUsuarios> SeguridadUsuarios { get; set; }
        public virtual DbSet<Supervisor> Supervisor { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tallas> Tallas { get; set; }
        public virtual DbSet<Tarifas> Tarifas { get; set; }
        public virtual DbSet<Tarjetas> Tarjetas { get; set; }
        public virtual DbSet<Tiendas> Tiendas { get; set; }
        public virtual DbSet<TipoCambio> TipoCambio { get; set; }
        public virtual DbSet<TipoPagos> TipoPagos { get; set; }
        public virtual DbSet<TiposMovimientos> TiposMovimientos { get; set; }
        public virtual DbSet<Vendedores> Vendedores { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<TomaFisica> TomaFisica { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<ZVentasxVendedor> ZVentasxVendedor { get; set; }
        public virtual DbSet<SeguridadRolesModulos> SeguridadRolesModulos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abonos>()
                .Property(e => e.CodCliente)
                .IsUnicode(false);

            modelBuilder.Entity<Abonos>()
                .Property(e => e.MtoEfectivo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Abonos>()
                .Property(e => e.MtoOtros)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Abonos>()
                .Property(e => e.MtoTarjeta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ActividadesEconomicas>()
                .Property(e => e.CodActividad)
                .IsUnicode(false);

            modelBuilder.Entity<ActividadesEconomicas>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Bancos>()
                .Property(e => e.CodBanco)
                .IsUnicode(false);

            modelBuilder.Entity<Bancos>()
                .Property(e => e.NomBanco)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora>()
                .Property(e => e.Suceso)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora>()
                .Property(e => e.Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora>()
                .Property(e => e.Computer)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora>()
                .Property(e => e.Sistema)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora>()
                .Property(e => e.UsuarioDB)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora>()
                .Property(e => e.Accion)
                .IsUnicode(false);

            //modelBuilder.Entity<Bodegas>()
            //    .HasMany(e => e.DetIngTiendas)
            //    .WithRequired(e => e.Bodegas)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cajas>()
                .Property(e => e.CodCaja)
                .IsUnicode(false);

            modelBuilder.Entity<Cajas>()
                .Property(e => e.NomCaja)
                .IsUnicode(false);

            //modelBuilder.Entity<Cajas>()
            //    .HasMany(e => e.CierreCaja)
            //    .WithRequired(e => e.Cajas)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cajeros>()
                .Property(e => e.CodCajero)
                .IsUnicode(false);

            modelBuilder.Entity<Cajeros>()
                .Property(e => e.NomCajero)
                .IsUnicode(false);

            modelBuilder.Entity<Cajeros>()
                .Property(e => e.ClavePaso)
                .IsUnicode(false);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.CodCajero)
                .IsUnicode(false);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.CodCaja)
                .IsUnicode(false);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.CodSupervisor)
                .IsUnicode(false);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.Efectivo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.Cheques)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.Tarjetas)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.OtrosPagos)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.MtoVendido)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.Diferencia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.MontoApertura)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.CortesEfectivo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.CortesDescripcion)
                .IsUnicode(false);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.Dolares)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.TipoCambio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CierreCaja>()
                .Property(e => e.IP)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.CodCliente)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.NomCliente)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.MtoCompra)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.MtoAbono)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.MarcaVehiculo)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.ModeloVehiculo)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.TipoCedula)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Clientes>()
                .Property(e => e.CodClienteGaxpar)
                .IsUnicode(false);

            //modelBuilder.Entity<Clientes>()
            //    .HasMany(e => e.Abonos)
            //    .WithRequired(e => e.Clientes)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Clientes>()
            //    .HasMany(e => e.EncApartado)
            //    .WithRequired(e => e.Clientes)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Clientes>()
            //    .HasMany(e => e.EncPedidos)
            //    .WithRequired(e => e.Clientes)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Clientes>()
            //    .HasMany(e => e.EncVtas)
            //    .WithRequired(e => e.Clientes)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<CodigosTarifaImpuestos>()
                .Property(e => e.CodTarifa)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CodigosTarifaImpuestos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            //modelBuilder.Entity<Departamentos>()
            //    .HasMany(e => e.Productos)
            //    .WithOptional(e => e.Departamentos)
            //    .HasForeignKey(e => e.CodDepto);

            modelBuilder.Entity<DetApartado>()
                .Property(e => e.CodPro)
                .IsUnicode(false);

            modelBuilder.Entity<DetApartado>()
                .Property(e => e.PrecioVenta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetApartado>()
                .Property(e => e.PorDescto)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetApartado>()
                .Property(e => e.CostoPro)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetApartado>()
                .Property(e => e.CodProHacienda)
                .IsUnicode(false);

            modelBuilder.Entity<DetCompras>()
                .Property(e => e.CodProveedor)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DetCompras>()
                .Property(e => e.CodPro)
                .IsUnicode(false);

            modelBuilder.Entity<DetCompras>()
                .Property(e => e.Cantidad)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DetCompras>()
                .Property(e => e.CostoPro)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetCompras>()
                .Property(e => e.ImptoVta)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DetCompras>()
                .Property(e => e.PorDescuento)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DetIngMercaderia>()
                .Property(e => e.CostoPro)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetIngMercaderia>()
                .Property(e => e.PrecioMayoreo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetIngTiendas>()
                .Property(e => e.CostoPro)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetIngTiendas>()
                .Property(e => e.PrecioMayoreo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetIngTiendas>()
                .Property(e => e.PrecioVenta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetMovInv>()
                .Property(e => e.CodMov)
                .IsUnicode(false);

            modelBuilder.Entity<DetMovInv>()
                .Property(e => e.CodPro)
                .IsUnicode(false);

            modelBuilder.Entity<DetMovInv>()
                .Property(e => e.CostoPro)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetPedidos>()
                .Property(e => e.CodPro)
                .IsUnicode(false);

            modelBuilder.Entity<DetPedidos>()
                .Property(e => e.PrecioVenta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetPedidos>()
                .Property(e => e.CostoPro)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetPedidos>()
                .Property(e => e.NomPro)
                .IsUnicode(false);

            modelBuilder.Entity<DetPedidos>()
                .Property(e => e.CodProHacienda)
                .IsUnicode(false);

            modelBuilder.Entity<DetVtas>()
                .Property(e => e.CodPro)
                .IsUnicode(false);

            modelBuilder.Entity<DetVtas>()
                .Property(e => e.PrecioVenta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetVtas>()
                .Property(e => e.Cantidad)
                .HasPrecision(18, 4);

            modelBuilder.Entity<DetVtas>()
                .Property(e => e.CostoPro)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DetVtas>()
                .Property(e => e.NomPro)
                .IsUnicode(false);

            modelBuilder.Entity<DetVtas>()
                .Property(e => e.CodProHacienda)
                .IsUnicode(false);

            modelBuilder.Entity<EncApartado>()
                .Property(e => e.CodCliente)
                .IsUnicode(false);

            modelBuilder.Entity<EncApartado>()
                .Property(e => e.SubTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncApartado>()
                .Property(e => e.Descuento)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncApartado>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncApartado>()
                .Property(e => e.AbonoCompra)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncApartado>()
                .Property(e => e.CodVendedor)
                .IsFixedLength()
                .IsUnicode(false);

            //modelBuilder.Entity<EncApartado>()
            //    .HasMany(e => e.DetApartado)
            //    .WithRequired(e => e.EncApartado)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<EncCompras>()
                .Property(e => e.CodProveedor)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EncCompras>()
                .Property(e => e.SubTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncCompras>()
                .Property(e => e.ImptoVta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncCompras>()
                .Property(e => e.TotalDescuento)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncCompras>()
                .Property(e => e.TotalCompra)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncIngTiendas>()
                .Property(e => e.TotalDocto)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncMovInv>()
                .Property(e => e.CodMov)
                .IsUnicode(false);

            modelBuilder.Entity<EncMovInv>()
                .Property(e => e.Detalle)
                .IsUnicode(false);

            modelBuilder.Entity<EncMovInv>()
                .Property(e => e.TotalMov)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.CodCliente)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.NomCliente)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.SubTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.Descuento)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.ImptoVentas)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.CodVendedor)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.CodCajero)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.MtoEfectivo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.CodBanco)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.MtoCheque)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.CodTarjeta)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.MtoTarjeta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.CodPago)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.MtoPago)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.CodCaja)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.MotivoAnulacion)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.CodSupervisor)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.MtoDol)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.CambioAbono)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.FAClave)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.FAConsecutivo)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.FAError)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.FAResolucion)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.TipoCambio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.FAClaveHacienda)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.FANotaCreditoConsecutivo)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.FANotaCreditoClaveHacienda)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.FAJson)
                .IsUnicode(false);

            modelBuilder.Entity<EncPedidos>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            //modelBuilder.Entity<EncPedidos>()
            //    .HasMany(e => e.DetPedidos)
            //    .WithRequired(e => e.EncPedidos)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.CodCliente)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.NomCliente)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.SubTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.Descuento)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.ImptoVentas)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.CodVendedor)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.CodCajero)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.MtoEfectivo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.CodBanco)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.MtoCheque)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.CodTarjeta)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.MtoTarjeta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.CodPago)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.MtoPago)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.CodCaja)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.MotivoAnulacion)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.CodSupervisor)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.MtoDol)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.CambioAbono)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.FAClave)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.FAConsecutivo)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.FAError)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.FAResolucion)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.TipoCambio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.FAClaveHacienda)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.FANotaCreditoConsecutivo)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.FANotaCreditoClaveHacienda)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.FAJson)
                .IsUnicode(false);

            modelBuilder.Entity<EncVtas>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            //modelBuilder.Entity<EncVtas>()
            //    .HasMany(e => e.DetVtas)
            //    .WithRequired(e => e.EncVtas)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<HistInv>()
                .Property(e => e.CodMov)
                .IsUnicode(false);

            modelBuilder.Entity<HistInv>()
                .Property(e => e.NumDocto)
                .IsUnicode(false);

            modelBuilder.Entity<HistInv>()
                .Property(e => e.CodPro)
                .IsUnicode(false);

            modelBuilder.Entity<HistInv>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<HistInv>()
                .Property(e => e.Costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HistInv>()
                .Property(e => e.CodProveedor)
                .IsUnicode(false);

            modelBuilder.Entity<HistProductos>()
                .Property(e => e.CodPro)
                .IsUnicode(false);

            modelBuilder.Entity<HistProductos>()
                .Property(e => e.MtoCompras)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HistProductos>()
                .Property(e => e.MtoVentas)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HistProductos>()
                .Property(e => e.Costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HistVentas>()
                .Property(e => e.CodVendedor)
                .IsUnicode(false);

            modelBuilder.Entity<HistVentas>()
                .Property(e => e.MtoVentas)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HistVentas>()
                .Property(e => e.CostoPro)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ImprimirFacturas>()
                .Property(e => e.CodCaja)
                .IsUnicode(false);

            modelBuilder.Entity<NotasCredito>()
                .Property(e => e.Monto)
                .HasPrecision(19, 4);

            modelBuilder.Entity<NotasCredito>()
                .Property(e => e.Abono)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrdenesExpress>()
                .Property(e => e.NomCliente)
                .IsUnicode(false);

            modelBuilder.Entity<OrdenesExpress>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<OrdenesExpress>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Parqueo>()
                .Property(e => e.NumPlaca)
                .IsUnicode(false);

            modelBuilder.Entity<Parqueo>()
                .Property(e => e.Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Parqueo>()
                .Property(e => e.MontoCobrado)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Parqueo>()
                .Property(e => e.TipoVehiculo)
                .IsUnicode(false);

            modelBuilder.Entity<Precios>()
                .Property(e => e.PrecioVenta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Productos>()
                .Property(e => e.CodPro)
                .IsUnicode(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.NomPro)
                .IsUnicode(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.CodBarras)
                .IsUnicode(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.CostoPro)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Productos>()
                .Property(e => e.PrecioVenta)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Productos>()
                .Property(e => e.UniMed)
                .IsUnicode(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.CostoIni)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Productos>()
                .Property(e => e.CostoAnt)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Productos>()
                .Property(e => e.CostoAct)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Productos>()
                .Property(e => e.CostoFob)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Productos>()
                .Property(e => e.PrecioMayor)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Productos>()
                .Property(e => e.MtoCompras)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Productos>()
                .Property(e => e.MtoVtas)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Productos>()
                .Property(e => e.NombreSeccion)
                .IsUnicode(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.Imagen)
                .IsUnicode(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.CodImpuestoTarifa)
                .IsUnicode(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.CodProHacienda)
                .IsUnicode(false);

            modelBuilder.Entity<Productos>()
                .Property(e => e.CabysDescripcion)
                .IsUnicode(false);

            modelBuilder.Entity<ProductosPrecios>()
                .Property(e => e.CodPro)
                .IsUnicode(false);

            modelBuilder.Entity<ProductosPreciosClientes>()
                .Property(e => e.CodPro)
                .IsUnicode(false);

            modelBuilder.Entity<ProductosPreciosClientes>()
                .Property(e => e.CodCliente)
                .IsUnicode(false);

            modelBuilder.Entity<ProductosPreciosLista>()
                .Property(e => e.NomLista)
                .IsUnicode(false);

            modelBuilder.Entity<Proveedores>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Proveedores>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<Proveedores>()
                .Property(e => e.MtoCompras)
                .HasPrecision(19, 4);

            //modelBuilder.Entity<Proveedores>()
            //    .HasMany(e => e.EncIngTiendas)
            //    .WithRequired(e => e.Proveedores)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<SeguridadModulos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            //modelBuilder.Entity<SeguridadModulos>()
            //    .HasMany(e => e.SeguridadRoles)
            //    .WithMany(e => e.SeguridadModulos)
            //    .Map(m => m.ToTable("SeguridadRolesModulos").MapLeftKey("CodModulo").MapRightKey("CodRol"));

            modelBuilder.Entity<SeguridadRoles>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<SeguridadUsuarios>()
                .Property(e => e.CodCompania)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SeguridadUsuarios>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<SeguridadUsuarios>()
                .Property(e => e.NomUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<SeguridadUsuarios>()
                .Property(e => e.Clave)
                .IsUnicode(false);

            modelBuilder.Entity<SeguridadUsuarios>()
                .Property(e => e.Activo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tallas>()
                .HasMany(e => e.Precios)
                .WithRequired(e => e.Tallas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tarifas>()
                .Property(e => e.Tipo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tarifas>()
                .Property(e => e.TipoVehiculo)
                .IsUnicode(false);

            modelBuilder.Entity<Tarifas>()
                .Property(e => e.Precio)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Tarjetas>()
                .Property(e => e.CodTarjeta)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tarjetas>()
                .Property(e => e.NomTarjeta)
                .IsUnicode(false);

            modelBuilder.Entity<Tarjetas>()
                .Property(e => e.EnteEmisor)
                .IsUnicode(false);

            modelBuilder.Entity<TipoCambio>()
                .Property(e => e.TipoCambio1)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Vendedores>()
                .Property(e => e.CodVendedor)
                .IsUnicode(false);

            modelBuilder.Entity<Vendedores>()
                .Property(e => e.NomVendedor)
                .IsUnicode(false);

            //modelBuilder.Entity<Vendedores>()
            //    .HasMany(e => e.EncPedidos)
            //    .WithRequired(e => e.Vendedores)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Vendedores>()
            //    .HasMany(e => e.EncVtas)
            //    .WithRequired(e => e.Vendedores)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Vendedores>()
            //    .HasMany(e => e.HistVentas)
            //    .WithRequired(e => e.Vendedores)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.Ubicacion)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.EmailDe)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.EmailPuerto)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.EmailUser)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.EmailPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.EmailServer)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.IP_WebSite)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.UrlAdministrativo)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.FACodEmpresa)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.FACodSucursal)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.FANIT)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.UrlWebApi)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.TipoIdentificacion)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.ResolucionHacienda)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.CodClienteDefault)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.CodVendedorDefault)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.CodigoActividadEconomica)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.ConexionGaxpar)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.CodEmpresaGaxpar)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.PrefijoRomanas)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.VersionTicket)
                .IsUnicode(false);

            modelBuilder.Entity<ZVentasxVendedor>()
                .Property(e => e.Precio)
                .HasPrecision(19, 4);
        }
    }
}
