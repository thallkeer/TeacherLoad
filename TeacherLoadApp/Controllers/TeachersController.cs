using System.Data;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;
using TeacherLoadApp.Models;

namespace TeacherLoadApp.Controllers
{
    public class TeachersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public TeachersController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IActionResult Index()
        {
            var teachers = unitOfWork.Teachers.GetAll().OrderBy(t => t.LastName);             
            return View("TeachersList",teachers);
        }

        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View("CreateTeacher");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
          [Bind("TeacherID,FirstName,LastName,Patronym,DepartmentID,PositionID")]
         Teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Teachers.Insert(teacher);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDropDownLists(teacher.DepartmentID,teacher.PositionID);
            return View("CreateTeacher",teacher);
        }

        public IActionResult Edit(int id)
        {
            Teacher teacher = unitOfWork.Teachers.GetByID(id);
            PopulateDropDownLists(teacher.DepartmentID,teacher.PositionID);
            return View("EditTeacher",teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("TeacherID,FirstName,LastName,Patronym,DepartmentID,PositionID")]
         Teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Teachers.Update(teacher);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }           
            return View("EditTeacher",teacher);
        }

        public IActionResult Delete(int id)
        {
            Teacher teacher = unitOfWork.Teachers.GetByID(id);
            return View("DeleteTeacher",teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = unitOfWork.Teachers.GetByID(id);
            unitOfWork.Teachers.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }       

        private void PopulateDropDownLists(object selectedDepartment = null,object selectedPosition = null)
        {
            var departmentsQuery = unitOfWork.Departments.Get().OrderBy(d => d.DepartmentName);
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "DepartmentName", selectedDepartment);
            var positionsQuery = unitOfWork.Positions.Get(orderBy: q => q.OrderBy(p => p.PositionName));
            ViewBag.PositionID = new SelectList(positionsQuery, "PositionID", "PositionName", selectedPosition);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
