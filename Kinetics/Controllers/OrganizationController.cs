using Microsoft.AspNetCore.Mvc;

namespace Kinetics.Controllers
{
    public class OrganizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
