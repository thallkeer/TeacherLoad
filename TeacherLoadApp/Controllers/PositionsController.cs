using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class PositionsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public PositionsController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        // GET: Positions
        public ActionResult Index()
        {
            var positions = unitOfWork.Positions.Get(orderBy: q => q.OrderBy(p => p.PositionName));
            return View("PositionsList", positions);
        }

        public IActionResult Create()
        {
            return View("CreatePosition");
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Position position)
        {
            if (unitOfWork.Positions.Get(p => p.PositionName == position.PositionName).Any())
            {
                ModelState.AddModelError("PositionName", "Такая должность уже существует!");
            }
            else if (ModelState.IsValid)
            {
                unitOfWork.Positions.Insert(position);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("CreatePosition", position);
        }

        // GET: Groups/Edit/5
        public IActionResult Edit(int id)
        {
            var position = unitOfWork.Positions.GetByID(id);
            if (position == null)
            {
                return NotFound();
            }
            return View("EditPosition", position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Position position)
        {
            if (id != position.PositionID)
            {
                return NotFound();
            }
            if (unitOfWork.Positions.Get(p => p.PositionName == position.PositionName).Any())
            {
                ModelState.AddModelError("PositionName", "Такая должность уже существует!");
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.Positions.Update(position);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (unitOfWork.Positions.GetByID(id) != null)
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
            return View("EditPosition", position);
        }

        // GET: Specialities/Delete/5
        public ActionResult Delete(int id)
        {
            var position = unitOfWork.Positions.GetByID(id);
            if (position == null)
            {
                return NotFound();
            }

            return View("DeletePosition", position);
        }

        // POST: Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var position = unitOfWork.Positions.GetByID(id);
            if (position.Teachers.Any())
            {
                ModelState.AddModelError("PositionID", "Нельзя удалять должность, пока на ней работают сотрудники!");
                return View("DeletePosition", position);
            }
            unitOfWork.Positions.Delete(position);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}