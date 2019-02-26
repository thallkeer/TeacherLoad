using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;
using TeacherLoadApp.Models;

namespace TeacherLoadApp.Controllers
{
    public class TeachersController : Controller
    {
        private UnitOfWork unitOfWork;
        public TeachersController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IActionResult Index()
        {
            var teachers = unitOfWork.Teachers.GetAll().OrderBy(t => t.LastName);            
            return View(teachers);
        }

        public IActionResult CreateTeacher()
        {
            PopulateDropDownLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeacher(
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
            return View(teacher);
        }

        public IActionResult EditTeacher(int id)
        {
            Teacher teacher = unitOfWork.Teachers.GetByID(id);
            PopulateDropDownLists(teacher.DepartmentID,teacher.PositionID);
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTeacher([Bind("TeacherID,FirstName,LastName,Patronym,DepartmentID,PositionID")]
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
            return View(teacher);
        }

        public IActionResult TeacherDetails(int id)
        {
            return View(unitOfWork.Teachers.GetByID(id));
        }

        public IActionResult DeleteTeacher(int id)
        {
            Teacher teacher = unitOfWork.Teachers.GetByID(id);
            return View(teacher);
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

        public IActionResult Query()
        {           
            var teachers = from teacher in unitOfWork.Teachers.GetAll()
                           join pl in unitOfWork.PersonalLoads.GetAll()
                           on teacher.TeacherID equals pl.TeacherID
                           where pl.IndividualClassID == 2
                           select teacher;
            
            return View(teachers);
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
