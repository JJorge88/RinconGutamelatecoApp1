using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using RinconGuatemaltecoApp.Data;
using RinconGuatemaltecoApp.Models;
using System.IO;
using System.Linq;

namespace RinconGuatemaltecoApp.Controllers
{
    public class FacturasController : Controller
    {
        private readonly RinconGuatemaltecoContext _context;

        public FacturasController(RinconGuatemaltecoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            try
            {
                var facturas = _context.Facturas
                    .Include(f => f.Venta)
                    .Include(f => f.Cliente)
                    .ToList();

                return View(facturas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar las facturas: {ex.Message}");
                return View(new List<Factura>()); // Retorna una lista vacía en caso de error
            }
        }

        public IActionResult Details(int id)
        {
            var factura = _context.Facturas
                .Include(f => f.Venta)
                .Include(f => f.Cliente)
                .FirstOrDefault(f => f.FacturaID == id);

            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        public IActionResult Delete(int id)
        {
            var factura = _context.Facturas.Find(id);

            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var factura = _context.Facturas.Find(id);

            if (factura != null)
            {
                _context.Facturas.Remove(factura);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult GenerarPDF(int id)
        {
            var factura = _context.Facturas
                .Include(f => f.Venta)
                .Include(f => f.Cliente)
                .Include(f => f.Venta.DetalleVentas)
                .ThenInclude(dv => dv.Producto)
                .FirstOrDefault(f => f.FacturaID == id);

            if (factura == null)
            {
                return NotFound("Factura no encontrada.");
            }

            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            var regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            document.Add(new Paragraph("Factura")
                .SetFont(boldFont)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20));

            document.Add(new Paragraph($"Factura ID: {factura.FacturaID}").SetFont(regularFont).SetFontSize(12));
            document.Add(new Paragraph($"Fecha: {factura.Fecha}").SetFont(regularFont).SetFontSize(12));
            document.Add(new Paragraph($"Cliente: {factura.Cliente.Nombre}").SetFont(regularFont).SetFontSize(12));
            document.Add(new Paragraph("\n"));

            var table = new Table(4).UseAllAvailableWidth();
            table.AddHeaderCell(new Cell().Add(new Paragraph("Producto").SetFont(boldFont)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Cantidad").SetFont(boldFont)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Precio").SetFont(boldFont)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Subtotal").SetFont(boldFont)));

            foreach (var detalle in factura.Venta.DetalleVentas)
            {
                table.AddCell(new Cell().Add(new Paragraph(detalle.Producto.Nombre).SetFont(regularFont)));
                table.AddCell(new Cell().Add(new Paragraph(detalle.Cantidad.ToString()).SetFont(regularFont)));
                table.AddCell(new Cell().Add(new Paragraph($"Q{detalle.Producto.Precio:N2}").SetFont(regularFont)));
                table.AddCell(new Cell().Add(new Paragraph($"Q{detalle.Subtotal:N2}").SetFont(regularFont)));
            }

            document.Add(table);

            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph($"Total: Q{factura.Total:N2}")
                .SetFont(boldFont)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(12));

            document.Close();

            return File(stream.ToArray(), "application/pdf", $"Factura_{factura.FacturaID}.pdf");
        }
    }
}
