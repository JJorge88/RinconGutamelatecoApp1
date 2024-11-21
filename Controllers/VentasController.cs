using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinconGuatemaltecoApp.Data;
using RinconGuatemaltecoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RinconGuatemaltecoApp.Controllers
{
    public class VentasController : Controller
    {
        private readonly RinconGuatemaltecoContext _context;

        public VentasController(RinconGuatemaltecoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var ventas = _context.Ventas
                    .Include(v => v.Cliente)
                    .ToList();

                return View(ventas);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error al cargar las ventas: {ex.Message}";
                return View(new List<Venta>());
            }
        }

        public IActionResult Create()
        {
            try
            {
                CargarClientesYProductos();
                return View(new Venta());
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error al cargar los datos: {ex.Message}";
                return View(new Venta());
            }
        }

        [HttpPost]
        public IActionResult Create(Venta venta, List<int> ProductoIDs, List<int> Cantidades)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    venta.Fecha = DateTime.Now;
                    venta.Total = 0;

                    // Guardar la venta inicial
                    _context.Ventas.Add(venta);
                    _context.SaveChanges();

                    // Procesar los detalles de la venta
                    for (int i = 0; i < ProductoIDs.Count; i++)
                    {
                        var producto = _context.Productos.Find(ProductoIDs[i]);
                        if (producto != null && producto.Stock >= Cantidades[i])
                        {
                            producto.Stock -= Cantidades[i];

                            var detalleVenta = new DetalleVenta
                            {
                                VentaID = venta.VentaID,
                                ProductoID = ProductoIDs[i],
                                Cantidad = Cantidades[i],
                                Subtotal = Cantidades[i] * producto.Precio
                            };

                            _context.DetalleVentas.Add(detalleVenta);
                            venta.Total += detalleVenta.Subtotal;
                        }
                        else
                        {
                            TempData["Error"] += $"Producto {producto?.Nombre ?? "desconocido"} no tiene suficiente stock o no existe.<br/>";
                        }
                    }

                    // Actualizar el total y guardar cambios
                    _context.SaveChanges();
                    TempData["Success"] = "Venta registrada exitosamente.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = $"Ocurrió un error al registrar la venta: {ex.Message}";
                }
            }
            else
            {
                // Mostrar los errores de validación que hayan ocurrido
                TempData["Error"] = "El modelo no es válido. Por favor, verifica los datos ingresados.";
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        TempData["Error"] += $"{error.ErrorMessage}<br/>";
                    }
                }
            }

            // Recargar los datos en caso de error
            CargarClientesYProductos();
            return View(venta);
        }

        public IActionResult Details(int id)
        {
            try
            {
                var venta = _context.Ventas
                    .Include(v => v.Cliente)
                    .Include(v => v.DetalleVentas)
                    .ThenInclude(d => d.Producto)
                    .FirstOrDefault(v => v.VentaID == id);

                if (venta == null)
                {
                    TempData["Error"] = "Venta no encontrada.";
                    return RedirectToAction("Index");
                }

                return View(venta);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error al cargar los detalles de la venta: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        // Método para cargar los ViewBags necesarios
        private void CargarClientesYProductos()
        {
            ViewBag.Clientes = _context.Clientes
                .Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.ClienteID.ToString(),
                    Text = c.Nombre
                })
                .ToList();

            ViewBag.Productos = _context.Productos
                .Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = p.ProductoID.ToString(),
                    Text = $"{p.Nombre} (Stock: {p.Stock})"
                })
                .ToList();
        }
    }
}
