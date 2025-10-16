using Microsoft.AspNetCore.Mvc;

namespace BTL_SchoolManager.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
