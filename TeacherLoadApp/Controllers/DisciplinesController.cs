using System;
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
            return PartialView("_CreateDiscipline");
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Discipline discipline)
        {
            if (IsExists(discipline))
            {
                ModelState.AddModelError("DisciplineName", "В базе данных уже существует дисциплина с таким названием");
            }
            else if (ModelState.IsValid)
            {
                unitOfWork.Disciplines.Insert(new Discipline { DisciplineName = discipline.DisciplineName });
                unitOfWork.Save();
                CreateNotification("Дисциплина добавлена!");
                //return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreateDiscipline", discipline);
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
            //return View("EditDiscipline",discipline);            
            return PartialView("_EditDiscipline",discipline);
        }

        private bool IsExists(Discipline discipline)
        {
            return unitOfWork.Disciplines.Get(d => d.DisciplineName == discipline.DisciplineName).Any();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Discipline discipline)
        {
            if (IsExists(discipline))
            {
                ModelState.AddModelError("DisciplineName", "В базе данных уже существует дисциплина с таким названием");
            }
            else if (ModelState.IsValid)
            {
                unitOfWork.Disciplines.Update(discipline);
                unitOfWork.Save();
                CreateNotification("Дисциплина успешно изменена!");
            }            
            return PartialView("_EditDiscipline", discipline);
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
            if (discipline != null)
            {
                if (discipline.GroupLoads.Any())
                {
                    ModelState.AddModelError("DisciplineID", "Нельзя удалять дисциплину, пока она есть в нагрузке у преподавателей!");
                    return View("DeleteDiscipline", discipline);
                }
                unitOfWork.Disciplines.Delete(discipline);
                var saved = false;
                while (!saved)
                {
                    try
                    {
                        // Attempt to save changes to the database
                        unitOfWork.Save();
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        foreach (var entry in ex.Entries)
                        {
                            if (entry.Entity is Discipline)
                            {
                                if (entry.GetDatabaseValues() == null)
                                    return View("DisciplinesList", unitOfWork.Disciplines.Get(orderBy: x => x.OrderBy(d => d.DisciplineName)));
                            }
                        }
                    }
                }
            }
            return View("DisciplinesList", unitOfWork.Disciplines.Get(orderBy: x => x.OrderBy(d => d.DisciplineName)));
        }     
    }
}