using Microsoft.AspNetCore.Mvc;

namespace CoderCarrer.Controllers
{
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
