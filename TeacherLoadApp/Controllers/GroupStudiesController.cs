using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class GroupStudiesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public IActionResult Create(GroupStudies groupStudy)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.GroupStudies.Insert(groupStudy);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("CreateGroupStudy", groupStudy);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int id)
        {
            var groupStudy = unitOfWork.GroupStudies.GetByID(id);
            if (groupStudy == null)
            {
                return NotFound();
            }
            return View("EditGroupStudy", groupStudy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GroupStudies groupStudy)
        {
            if (id != groupStudy.GroupClassID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.GroupStudies.Update(groupStudy);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (unitOfWork.GroupStudies.GetByID(id) != null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("EditGroupStudy", groupStudy);
        }

        // GET: Specialities/Delete/5
        public ActionResult Delete(int id)
        {
            var groupStudy = unitOfWork.GroupStudies.GetByID(id);
            if (groupStudy == null)
            {
                return NotFound();
            }

            return View("DeleteGroupStudy", groupStudy);
        }

        // POST: Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var groupStudy = unitOfWork.GroupStudies.GetByID(id);
            unitOfWork.GroupStudies.Delete(groupStudy);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}