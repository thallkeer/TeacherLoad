using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class SpecialitiesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public SpecialitiesController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        // GET: Specialities
        public ActionResult Index()
        {
            var specialities = unitOfWork.Specialities.Get(orderBy: q => q.OrderBy(s => s.Code));
            return View("SpecialitiesList", specialities);
        }

        // GET: Specialities/Create
        public ActionResult Create()
        {
            return View("CreateSpeciality");
        }

        // POST: Specialities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                if (unitOfWork.Specialities.GetByID(speciality.Code) != null)
                {
                    ModelState.AddModelError("Code", "Такая специальность уже существует!");
                }
                else
                {
                    unitOfWork.Specialities.Insert(speciality);
                    unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("CreateSpeciality", speciality);
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
            return View("EditSpeciality",speciality);
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
            return View("EditSpeciality",speciality);
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

            return View("DeleteSpeciality",speciality);
        }

        // POST: Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var speciality = unitOfWork.Specialities.GetByID(id);           
            if (speciality.Groups.Any())
            {
                ModelState.AddModelError("Code", "Нельзя удалять специальность, так как существуют группы, обучающиеся на ней!");
                return View("DeleteSpeciality", speciality);
            }        
            unitOfWork.Specialities.Delete(speciality);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}