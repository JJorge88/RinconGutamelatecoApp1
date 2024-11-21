namespace RinconGuatemaltecoApp.Models
{
    public class Devolucion
    {
        public int DevolucionID { get; set; }
        public int VentaID { get; set; }
        public Venta Venta { get; set; } = null!; // Evitar referencia nula
        public int ProductoID { get; set; }
        public Producto Producto { get; set; } = null!;
        public int Cantidad { get; set; }
        public string Motivo { get; set; } = string.Empty; // Evitar valores nulos
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
