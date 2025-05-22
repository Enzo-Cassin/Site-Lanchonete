using Microsoft.AspNetCore.Mvc;

namespace Site_Lanchonete.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
