using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            FillDataLists();
            return View();
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
            return View(load);
        }

        // GET: GroupLoads/Edit/5
        public ActionResult Edit([Bind("TeacherID,GroupNumber,DisciplineID,GroupStudiesID,StudyYear,StudyType,Semester")] GroupLoad item)
        {
            var load = unitOfWork.GroupLoads.GetByID(item);
            if (load == null)
            {
                return NotFound();
            }
            FillDataLists(item);
            return View(item);
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
            return View(load);
        }

        // GET: GroupLoads/Delete/5
        public ActionResult Delete(int id)
        {
            var load = unitOfWork.GroupLoads.GetAll();
            return View();
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