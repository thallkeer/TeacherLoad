using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;
using TeacherLoadApp.Models;

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
            return View("GroupLoadsList",loads);
        }

        public ActionResult TeachersGroupLoad(int teacherID)
        {
            List<TeachersListViewModel> teachers = new List<TeachersListViewModel>();
            foreach (Teacher teacher in unitOfWork.Teachers.Get())
            {
                teachers.Add(new TeachersListViewModel() { TeacherID = teacher.TeacherID, TeacherName = teacher.FullName });
            }

            if (teacherID == 0)
                teacherID = teachers.First().TeacherID;

            IEnumerable<GroupLoad> loads = 
                //unitOfWork.GroupLoads.GetAll().Where(x => x.TeacherID == teacherID).OrderBy(x => x.GroupNumber);
            unitOfWork.GroupLoads.GetAll().Where(x => x.TeacherID == teacherID)
                .GroupBy(l => new { l.DisciplineID, l.GroupStudiesID })
                .Select(g => g.First());
            ViewBag.Active = teacherID;
            return View(new TeacherLoadViewModel() { TeachersList = teachers, GroupLoads = loads.ToList() });
        }      

        // GET: GroupLoads/Create
        public ActionResult Create()
        {
            FillDataLists();
            return View("CreateGroupLoad");
        }

        // POST: GroupLoads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupLoad load)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.GroupLoads.Insert(load);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            FillDataLists(load);
            return View("CreateGroupLoad",load);
        }

        // GET: GroupLoads/Edit/5
        public ActionResult Edit(GroupLoad item)
        {
            var load = unitOfWork.GroupLoads.GetByID(item);
            if (load == null)
            {
                return NotFound();
            }
            FillDataLists(load);
            return View("EditGroupLoad",load);
        }

        // POST: GroupLoads/Edit/5
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(GroupLoad load)
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
            return View("EditGroupLoad",load);
        }

        // GET: GroupLoads/Delete/5
        public ActionResult Delete(int id)
        {
            var load = unitOfWork.GroupLoads.GetAll();
            return View("DeleteGroupLoad");
        }

        // POST: GroupLoads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var load = unitOfWork.GroupLoads.GetByID(id);
            unitOfWork.GroupLoads.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        private void FillDataLists(GroupLoad load = null)
        {
            if (load == null)
            {
                ViewData["Teachers"] = new SelectList(unitOfWork.Teachers.GetAll(), "TeacherID", "FullName");
                ViewData["Groups"] = new SelectList(unitOfWork.Groups.GetAll(), "GroupNumber", "GroupNumber");
                ViewData["Disciplines"] = new SelectList(unitOfWork.Disciplines.GetAll(), "DisciplineID", "DisciplineName");
                ViewData["GroupStudies"] = new SelectList(unitOfWork.GroupStudies.GetAll(), "GroupClassID", "GroupClassName");
            }
            else
            {
                ViewData["TeacherID"] = new SelectList(unitOfWork.Teachers.GetAll(), "TeacherID", "FullName", load.TeacherID);
                ViewData["GroupStudiesID"] = new SelectList(unitOfWork.GroupStudies.GetAll(), "GroupClassID", "GroupClassName", load.GroupStudiesID);
                ViewData["GroupNumber"] = new SelectList(unitOfWork.Groups.GetAll(), "GroupNumber", "GroupNumber", load.GroupNumber);
                ViewData["DisciplineID"] = new SelectList(unitOfWork.Disciplines.GetAll(), "DisciplineID", "DisciplineName", load.DisciplineID);
            }
        }
    }
}