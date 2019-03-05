using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class IndividualStudiesController : Controller
    {
        private UnitOfWork unitOfWork;

        public IndividualStudiesController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET: IndividualStudies
        public ActionResult Index()
        {
            var items = unitOfWork.IndividualStudies.GetAll();
            return View("IndividualStudiesList",items);
        }        
    }
}