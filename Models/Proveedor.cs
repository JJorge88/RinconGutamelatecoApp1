namespace RinconGuatemaltecoApp.Models
{
    public class Proveedor
    {
        public int ProveedorID { get; set; }
        public string Nombre { get; set; } = string.Empty; // Evitar valores nulos
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
    }
}
