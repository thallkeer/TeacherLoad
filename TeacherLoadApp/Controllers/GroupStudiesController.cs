using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class GroupStudiesController : Controller
    {
        private UnitOfWork unitOfWork;

        public GroupStudiesController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        // GET: GroupStudies
        public ActionResult Index()
        {
            var items = unitOfWork.GroupStudies.GetAll();
            return View("GroupStudiesList",items);
        }
    }
}