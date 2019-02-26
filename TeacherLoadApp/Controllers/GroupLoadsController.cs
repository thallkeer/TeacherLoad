using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class GroupLoadsController : Controller
    {
        private UnitOfWork unitOfWork;
        public GroupLoadsController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IActionResult Index()
        {
            var loads = unitOfWork.GroupLoads.GetAll();           
            return View(loads);
        }

        // GET: GroupLoads/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GroupLoads/Create
        public ActionResult Create()
        {
            ViewData["Teachers"] = new SelectList(unitOfWork.Teachers.GetAll(), "TeacherID", "FullName");
            ViewData["Groups"] = new SelectList(unitOfWork.Groups.Get(includeProperties: "Speciality"), "GroupNumber", "GroupNumber");
            ViewData["Disciplines"] = new SelectList(unitOfWork.Disciplines.Get(), "DisciplineID", "DisciplineName");
            ViewData["GroupStudies"] = new SelectList(unitOfWork.GroupStudies.Get(), "ID", "ClassType");
            return View();
        }

        // POST: GroupLoads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupLoads/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var load = unitOfWork.GroupLoads.GetByID(id);
            if (load == null)
            {
                return NotFound();
            }
            ViewData["TeacherID"] = new SelectList(unitOfWork.Teachers.Get(), "TeacherID", "FullName", load.TeacherID);
            ViewData["GroupStudiesID"] = new SelectList(unitOfWork.GroupStudies.Get(), "GroupClassID", "GroupClassName", load.GroupStudiesID);
            ViewData["GroupNumber"] = new SelectList(unitOfWork.Groups.Get(), "GroupNumber", "GroupNumber", load.GroupNumber);
            ViewData["DisciplineID"] = new SelectList(unitOfWork.Disciplines.Get(), "DisciplineID", "DisciplineName", load.DisciplineID);
            return View(load);
        }

        // POST: GroupLoads/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupLoad load)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.GroupLoads.Update(load);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(load);
        }

        // GET: GroupLoads/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GroupLoads/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}