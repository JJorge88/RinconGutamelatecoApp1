namespace RinconGuatemaltecoApp.Models
{
    public class Pago
    {
        public int PagoID { get; set; }
        public int VentaID { get; set; }
        public Venta Venta { get; set; } = null!;
        public int MetodoPagoID { get; set; }
        public MetodoPago MetodoPago { get; set; } = null!;
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
