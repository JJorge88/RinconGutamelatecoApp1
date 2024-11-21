using Microsoft.EntityFrameworkCore;
using RinconGuatemaltecoApp.Models;

namespace RinconGuatemaltecoApp.Data
{
    public class RinconGuatemaltecoContext : DbContext
    {
        public RinconGuatemaltecoContext(DbContextOptions<RinconGuatemaltecoContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Proveedor> Proveedores { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Venta> Ventas { get; set; } = null!; // Cambié a "Ventas" para coincidir con las convenciones de pluralización de EF.
        public DbSet<DetalleVenta> DetalleVentas { get; set; } = null!;
        public DbSet<Devolucion> Devoluciones { get; set; } = null!;
        public DbSet<MetodoPago> MetodosPago { get; set; } = null!;
        public DbSet<Pago> Pagos { get; set; } = null!;
        public DbSet<Factura> Facturas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de claves primarias
            modelBuilder.Entity<DetalleVenta>()
                .HasKey(d => d.DetalleID);

            modelBuilder.Entity<Venta>().ToTable("Venta");
            modelBuilder.Entity<Producto>().ToTable("Productos");
            modelBuilder.Entity<Categoria>().ToTable("Categorias");
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones
            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.DetalleVentas)
                .HasForeignKey(d => d.VentaID)
                .OnDelete(DeleteBehavior.Restrict); // Evitar cascada en esta relación

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Venta)
                .WithMany(v => v.Facturas) // Asegúrate de que `Facturas` exista en la clase Venta
                .HasForeignKey(f => f.VentaID)
                .OnDelete(DeleteBehavior.Restrict); // Evitar cascada para evitar ciclos

            // Configurar precisión para decimales
            modelBuilder.Entity<DetalleVenta>()
                .Property(d => d.Subtotal)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Factura>()
                .Property(f => f.Total)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Pago>()
                .Property(p => p.Monto)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Venta>()
                .Property(v => v.Total)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
