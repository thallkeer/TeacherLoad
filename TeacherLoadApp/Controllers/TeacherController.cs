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
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork;
        public HomeController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        public IActionResult Index()
        {
            var teachers = unitOfWork.TeacherService.GetAll().OrderBy(t => t.LastName);
            return View(teachers);
        }

        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
          /*[Bind(Include = "CourseID,Title,Credits,DepartmentID")]*/
         Teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.TeacherService.Add(teacher);
                    unitOfWork.TeacherService.Save();
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

        public IActionResult Edit(int id)
        {
            Teacher teacher = unitOfWork.TeacherService.GetById(id);
            PopulateDropDownLists(teacher.DepartmentID,teacher.PositionID);
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(/*[Bind(Include = "CourseID,Title,Credits,DepartmentID")]*/
         Teacher teacher)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.TeacherService.Update(teacher);
                    unitOfWork.TeacherService.Save();
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

        public IActionResult Details(int id)
        {
            return View(unitOfWork.TeacherService.GetById(id));
        }

        public IActionResult Delete(int id)
        {
            Teacher teacher = unitOfWork.TeacherService.GetById(id);
            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = unitOfWork.TeacherService.GetById(id);
            unitOfWork.TeacherService.Delete(id);
            unitOfWork.TeacherService.Save();
            return RedirectToAction("Index");
        }

        private void PopulateDropDownLists(object selectedDepartment = null,object selectedPosition = null)
        {
            var departmentsQuery = unitOfWork.DepartmentsService.Get().OrderBy(d => d.DepartmentName);
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "DepartmentName", selectedDepartment);
            var positionsQuery = unitOfWork.PositionsService.Get().OrderBy(p => p.PositionName);
            ViewBag.PositionID = new SelectList(positionsQuery, "PositionID", "PositionName", selectedPosition);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
