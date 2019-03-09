using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class DisciplinesController : Controller
    {
        private UnitOfWork unitOfWork;

        public DisciplinesController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        // GET: Disciplines
        public ActionResult Index()
        {
            var disciplines = unitOfWork.Disciplines.Get(orderBy: q => q.OrderBy(d => d.DisciplineName));
            return View("DisciplinesList",disciplines);
        }

        public ActionResult Create()
        {           
            return View("CreateDiscipline");
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Disciplines.Insert(discipline);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("CreateDiscipline",discipline);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int id)
        {
            var discipline = unitOfWork.Disciplines.GetByID(id);
            if (discipline == null)
            {
                return NotFound();
            }           
            return View("EditDiscipline",discipline);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Discipline discipline)
        {
            if (id != discipline.DisciplineID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.Disciplines.Update(discipline);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (unitOfWork.Disciplines.GetByID(id) != null)
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
            return View("EditDiscipline", discipline);
        }

        // GET: Specialities/Delete/5
        public ActionResult Delete(int id)
        {
            var discipline = unitOfWork.Disciplines.GetByID(id);
            if (discipline == null)
            {
                return NotFound();
            }

            return View("DeleteDiscipline", discipline);
        }

        // POST: Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var discipline = unitOfWork.Disciplines.GetByID(id);
            unitOfWork.Disciplines.Delete(discipline);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}