using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class PositionsController : Controller
    {
        private UnitOfWork unitOfWork;

        public PositionsController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        // GET: Positions
        public ActionResult Index()
        {
            var positions = unitOfWork.Positions.Get(orderBy: q => q.OrderBy(p => p.PositionName));
            return View("PositionsList",positions);
        }        
    }
}