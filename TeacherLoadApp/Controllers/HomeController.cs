using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;
using TeacherLoadApp.Models;

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