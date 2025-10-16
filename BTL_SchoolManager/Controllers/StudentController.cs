using Microsoft.AspNetCore.Mvc;

namespace BTL_SchoolManager.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
