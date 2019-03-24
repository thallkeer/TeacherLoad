using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;
using TeacherLoadApp.Models;

namespace TeacherLoadApp.Controllers
{
    public class PersonalLoadsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public PersonalLoadsController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET: PersonalLoads
        public ActionResult Index()
        {
            var loads = unitOfWork.PersonalLoads.GetAll().OrderBy(pl => pl.Teacher.LastName);
            var classTypes = new SelectList(unitOfWork.IndividualStudies.GetAll(), "IndividualClassID", "IndividualClassName");

            List<GroupingVM<PersonalLoad>> grouped = loads.GroupBy(x => x.Teacher.FullName)
                .Select(g => new GroupingVM<PersonalLoad> { Key = g.Key, Values = g.ToList() }).ToList();

            var model = new TeacherPersonalLoadVM
            {
                PersonalStudies = classTypes,
                PersonalLoads = loads.ToList(),
                GroupedLoads = grouped
            };
            return View("PersonalLoadsList",model);
        }

        public IActionResult TeacherLoadByClassType(int classID)
        {
            var loads = classID != 0 ? unitOfWork.PersonalLoads.Get(pl => pl.IndividualClassID == classID
                , q => q.OrderBy(pl => pl.Teacher.LastName),"Teacher") : unitOfWork.PersonalLoads.GetAll().OrderBy(pl => pl.Teacher.LastName);

            List<GroupingVM<PersonalLoad>> grouped = loads.GroupBy(x => x.Teacher.FullName)
                .Select(g => new GroupingVM<PersonalLoad>{ Key=g.Key, Values = g.ToList() }).ToList();
            
            var classTypes = new SelectList(unitOfWork.IndividualStudies.GetAll(), "IndividualClassID", "IndividualClassName", classID);
            
            return PartialView("PersonalLoadPartial",grouped);
        }

        public int CalculateHours(int classID,int count)
        {
            IndividualStudies studyType = unitOfWork.IndividualStudies.GetByID(classID);
            return studyType.VolumeByPerson * count;
        }

        // GET: GroupLoads/Create
        public ActionResult Create()
        {
            var model = BuildModel();
            return View("CreatePersonalLoad",model);
        }

        // POST: GroupLoads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonalLoadVM loadModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.PersonalLoads.Insert(loadModel.PersonalLoad);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            var model = BuildModel(loadModel.PersonalLoad);
            return View("CreatePersonalLoad",model);
        }

        private PersonalLoadVM BuildModel(PersonalLoad personalLoad = null)
        {
            if (personalLoad == null)
                personalLoad = new PersonalLoad();

            return new PersonalLoadVM
            {
                PersonalLoad = personalLoad,
                Teachers = new SelectList(unitOfWork.Teachers.GetAll(), "TeacherID", "FullName", personalLoad.TeacherID),
                Classes = new SelectList(unitOfWork.IndividualStudies.GetAll(), "IndividualClassID", "IndividualClassName", personalLoad.IndividualClassID)
            };            
        }     

        // GET: GroupLoads/Edit/5
        public ActionResult Edit(int id)
        {
            var load = unitOfWork.PersonalLoads.GetByID(id);
            if (load == null)
            {
                return NotFound();
            }
            var model = BuildModel(load);
            return View("EditPersonalLoad",model);
        }

        // POST: GroupLoads/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(PersonalLoadVM load)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.PersonalLoads.Update(load.PersonalLoad);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View("EditPersonalLoad",BuildModel(load.PersonalLoad));
        }

        // GET: GroupLoads/Delete/5
        public ActionResult Delete(int id)
        {
            var load = unitOfWork.PersonalLoads.GetByID(id);
            return View("DeletePersonalLoad",load);
        }

        // POST: GroupLoads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var load = unitOfWork.PersonalLoads.GetByID(id);
            unitOfWork.PersonalLoads.Delete(load);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }        
    }
}