using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class DisciplinesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public DisciplinesController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        // GET: Disciplines
        public ActionResult Index()
        {
            var disciplines = unitOfWork.Disciplines.Get(orderBy: q => q.OrderBy(d => d.DisciplineName));
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("_Disciplines", disciplines);
            }

            return View("DisciplinesList",disciplines);
        }

        public ActionResult Create()
        {
            return PartialView("CreateDisciplinePartial");
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Discipline discipline)
        {
            if (unitOfWork.Disciplines.Get(d => d.DisciplineName == discipline.DisciplineName).FirstOrDefault() != null)
            {
                ModelState.AddModelError(discipline.DisciplineName, "В базе данных уже существует запись с таким названием");
            }
            else if (ModelState.IsValid)
            {
                unitOfWork.Disciplines.Insert(new Discipline { DisciplineName = discipline.DisciplineName });
                unitOfWork.Save();
                CreateNotification("Дисциплина добавлена!");
                //return RedirectToAction(nameof(Index));
            }            
            return PartialView("CreateDisciplinePartial", discipline);
        }

        [NonAction]
        private void CreateNotification(string message)
        {
            TempData.TryGetValue("Notifications", out object value);
            var notifications = value as List<string> ?? new List<string>();
            notifications.Add(message);
            TempData["Notifications"] = notifications;
        }

        public IActionResult Notifications()
        {
            TempData.TryGetValue("Notifications", out object value);
            var notifications = value as IEnumerable<string> ?? Enumerable.Empty<string>();
            return PartialView("_NotificationsPartial", notifications);
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