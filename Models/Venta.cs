using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RinconGuatemaltecoApp.Models
{
    public class Venta
    {
        public int VentaID { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio.")]
        public Cliente Cliente { get; set; } = null!; // Evita valores nulos

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a cero.")]
        public decimal Total { get; set; }

        public ICollection<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();

        public ICollection<Factura>? Facturas { get; set; }
    }
}
