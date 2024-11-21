using System.ComponentModel.DataAnnotations.Schema;

namespace RinconGuatemaltecoApp.Models
{
    [Table("Productos")]
    public class Producto
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}



