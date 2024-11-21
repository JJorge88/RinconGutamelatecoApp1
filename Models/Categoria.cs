using System.ComponentModel.DataAnnotations.Schema;

namespace RinconGuatemaltecoApp.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
