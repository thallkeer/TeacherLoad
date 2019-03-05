using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class DisciplinesController : Controller
    {
        private UnitOfWork unitOfWork;

        public DisciplinesController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        // GET: Disciplines
        public ActionResult Index()
        {
            var disciplines = unitOfWork.Disciplines.Get(orderBy: q => q.OrderBy(d => d.DisciplineName));
            return View("DisciplinesList",disciplines);
        }      
    }
}