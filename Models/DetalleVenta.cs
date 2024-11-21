using System.ComponentModel.DataAnnotations.Schema;

namespace RinconGuatemaltecoApp.Models
{
    public class DetalleVenta
    {
        public int DetalleID { get; set; } // Clave primaria

        public int VentaID { get; set; } // Clave foránea
        public Venta Venta { get; set; } = null!;

        public int ProductoID { get; set; } // Clave foránea
        public Producto Producto { get; set; } = null!;

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Subtotal { get; set; }
    }
}
