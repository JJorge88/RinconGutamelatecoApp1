namespace RinconGuatemaltecoApp.Models
{
    public class MetodoPago
    {
        public int MetodoPagoID { get; set; }
        public string TipoPago { get; set; } = string.Empty; // Evitar valores nulos
    }
}
