namespace RinconGuatemaltecoApp.Models
{
    public class Factura
    {
        public int FacturaID { get; set; }
        public int VentaID { get; set; }
        public Venta Venta { get; set; } = null!;
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
    }
}
