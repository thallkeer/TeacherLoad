using Microsoft.AspNetCore.Mvc;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork;

        public HomeController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}