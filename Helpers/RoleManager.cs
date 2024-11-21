using Microsoft.AspNetCore.Http;

namespace RinconGuatemaltecoApp.Helpers
{
    public static class RoleManager
    {
        public static bool IsAdmin(IHttpContextAccessor httpContextAccessor)
        {
            var rol = httpContextAccessor.HttpContext?.Session.GetString("Rol");
            return rol != null && rol == "Admin";
        }

        public static bool IsVendedor(IHttpContextAccessor httpContextAccessor)
        {
            var rol = httpContextAccessor.HttpContext?.Session.GetString("Rol");
            return rol != null && rol == "Vendedor";
        }
    }
}
