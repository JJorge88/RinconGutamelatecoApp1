using Microsoft.AspNetCore.Mvc;

namespace RinconGuatemaltecoApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Menu()
        {
            // Validar sesión
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Usuario = HttpContext.Session.GetString("Usuario");
            ViewBag.Rol = HttpContext.Session.GetString("Rol");

            return View();
        }
    }
}
