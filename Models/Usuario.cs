namespace RinconGuatemaltecoApp.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; } = string.Empty; // Evitar valores nulos
        public string Contraseña { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
