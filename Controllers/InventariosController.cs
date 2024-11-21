using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinconGuatemaltecoApp.Data;
using RinconGuatemaltecoApp.Models;

namespace RinconGuatemaltecoApp.Controllers
{
    public class InventariosController : Controller
    {
        private readonly RinconGuatemaltecoContext _context;

        public InventariosController(RinconGuatemaltecoContext context)
        {
            _context = context;
        }

        // Método para mostrar el inventario
        public IActionResult Index()
        {
            var productos = _context.Productos.ToList();
            return View(productos);
        }

        // Método para editar el stock de un producto
        public IActionResult Edit(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        public IActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Update(producto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }
    }
}
