using Microsoft.AspNetCore.Mvc;

namespace Site_Lanchonete.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) { return View(); }
            return RedirectToAction("Login", "Account");
        }
    }
}
