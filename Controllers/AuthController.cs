using Microsoft.AspNetCore.Mvc;
using RinconGuatemaltecoApp.Data;
using RinconGuatemaltecoApp.Models;

namespace RinconGuatemaltecoApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly RinconGuatemaltecoContext _context;

        public AuthController(RinconGuatemaltecoContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == username && u.Contraseña == password);

            if (user != null)
            {
                // Guardar datos del usuario en sesión
                HttpContext.Session.SetString("Usuario", user.NombreUsuario);
                HttpContext.Session.SetString("Rol", user.Rol);

                return RedirectToAction("Menu", "Home");
            }

            ViewBag.Error = "Credenciales inválidas.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
