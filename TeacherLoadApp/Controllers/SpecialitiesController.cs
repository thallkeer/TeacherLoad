using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class SpecialitiesController : Controller
    {
        private UnitOfWork unitOfWork;

        public SpecialitiesController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        // GET: Specialities
        public ActionResult Index()
        {
            var specialities = unitOfWork.Specialities.Get(orderBy: q => q.OrderBy(s => s.Code));
            return View(specialities);
        }

        // GET: Specialities/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Specialities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Specialities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Specialities.Insert(speciality);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }           
            return View(speciality);
        }

        // GET: Specialities/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciality = unitOfWork.Specialities.GetByID(id);
            if (speciality == null)
            {
                return NotFound();
            }
            return View(speciality);
        }

        // POST: Specialities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Speciality speciality)
        {
            if (id != speciality.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.Specialities.Update(speciality);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (unitOfWork.Specialities.GetByID(id) != null)
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
            return View(speciality);
        }

        // GET: Specialities/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speciality = unitOfWork.Specialities.GetByID(id);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        // POST: Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var speciality = unitOfWork.Specialities.GetByID(id);
            unitOfWork.Specialities.Delete(speciality);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}