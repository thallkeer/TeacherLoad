using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(TeacherLoadContext context, SignInManager<ApplicationUser> signInManager)
        {
            unitOfWork = new UnitOfWork(context);
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return View();
            }
            return RedirectToAction("Login", "Users");
        }
    }
}