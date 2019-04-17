using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;
using System.Linq;

namespace TeacherLoadApp.Controllers
{
    public class IndividualStudiesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public IndividualStudiesController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET: IndividualStudies
        public ActionResult Index()
        {
            var items = unitOfWork.IndividualStudies.GetAll();
            return View("IndividualStudiesList",items);
        }

        public IActionResult Create()
        {
            return View("CreateIndividualStudy");
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IndividualStudies individualStudy)
        {        
            if (Exists(individualStudy))
            {
                ModelState.AddModelError("IndividualClassName", "Такой вид индивидуальных занятий уже существует!");
            }
            else if (ModelState.IsValid)
            {
                unitOfWork.IndividualStudies.Insert(individualStudy);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("CreateIndividualStudy", individualStudy);
        }

        // GET: Groups/Edit/5
        public IActionResult Edit(int id)
        {
            var individualStudy = unitOfWork.IndividualStudies.GetByID(id);
            if (individualStudy == null)
            {
                return NotFound();
            }
            return View("EditIndividualStudy", individualStudy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IndividualStudies individualStudy)
        {
            if (id != individualStudy.IndividualClassID)
            {
                return NotFound();
            }
            if (Exists(individualStudy))
            {
                ModelState.AddModelError("IndividualClassName", "Такой вид индивидуальных занятий уже существует!");
            }
            else if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.IndividualStudies.Update(individualStudy);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (unitOfWork.IndividualStudies.GetByID(id) != null)
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
            return View("EditIndividualStudy", individualStudy);
        }

        // GET: Specialities/Delete/5
        public ActionResult Delete(int id)
        {
            var individualStudy = unitOfWork.IndividualStudies.GetByID(id);
            if (individualStudy == null)
            {
                return NotFound();
            }

            return View("DeleteIndividualStudy", individualStudy);
        }

        // POST: Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var individualStudy = unitOfWork.IndividualStudies.GetByID(id);
            if (individualStudy.PersonalLoads.Any())
            {
                ModelState.AddModelError("IndividualClassID", "Нельзя удалять вид индивидуальных занятий, пока он задействован в нагрузке преподавателя!");
                return View("DeleteIndividualStudy", individualStudy);
            }
            unitOfWork.IndividualStudies.Delete(individualStudy);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool Exists(IndividualStudies individualStudy)
        {
            return unitOfWork.IndividualStudies.Get(indStudy => indStudy.IndividualClassName == individualStudy.IndividualClassName).Any();
        }
    }
}