using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
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
            var loads = unitOfWork.PersonalLoads.GetAll();
            var classTypes = new SelectList(unitOfWork.IndividualStudies.Get(), "IndividualClassID", "IndividualClassName");
            List<GroupingViewModel<PersonalLoad>> grouped = loads.GroupBy(x => x.Teacher.FullName)
                .Select(g => new GroupingViewModel<PersonalLoad> { Key = g.Key, Values = g.ToList() }).ToList();

            var model = new TeacherPersonalLoadViewModel
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
                , q => q.OrderBy(pl => pl.Teacher.LastName),"Teacher") : unitOfWork.PersonalLoads.GetAll();

            List<GroupingViewModel<PersonalLoad>> grouped = loads.GroupBy(x => x.Teacher.FullName)
                .Select(g => new GroupingViewModel<PersonalLoad>{ Key=g.Key, Values = g.ToList() }).ToList();
            
            var classTypes = new SelectList(unitOfWork.IndividualStudies.Get(), "IndividualClassID", "IndividualClassName", classID);

            var model = new TeacherPersonalLoadViewModel
            {
                PersonalStudies = classTypes,
                GroupedLoads = grouped
            };
            
            return PartialView("LoadByClassTypePartial",grouped);
        }

        public int CalculateHours(int classID,int count)
        {
            IndividualStudies studyType = unitOfWork.IndividualStudies.GetByID(classID);
            return studyType.VolumeByPerson * count;
        }

        // GET: GroupLoads/Create
        public ActionResult Create()
        {
            FillDataLists();
            return View("CreatePersonalLoad");
        }

        // POST: GroupLoads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonalLoad personalLoad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.PersonalLoads.Insert(personalLoad);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            FillDataLists(personalLoad);
            return View("CreatePersonalLoad",personalLoad);
        }

        // GET: GroupLoads/Edit/5
        public ActionResult Edit(int teacherID, int classID)
        {
            var load = unitOfWork.PersonalLoads.GetByKeys(teacherID,classID);
            if (load == null)
            {
                return NotFound();
            }
            FillDataLists(load);
            return View("EditPersonalLoad",load);
        }

        // POST: GroupLoads/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(PersonalLoad load)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.PersonalLoads.Update(load);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View("EditPersonalLoad",load);
        }

        // GET: GroupLoads/Delete/5
        public ActionResult Delete(int teacherID,int classID)
        {
            var load = unitOfWork.PersonalLoads.GetByKeys(teacherID,classID);
            return View("DeletePersonalLoad",load);
        }

        // POST: GroupLoads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int teacherID, int classID)
        {
            var load = unitOfWork.PersonalLoads.GetByKeys(teacherID, classID);
            unitOfWork.PersonalLoads.Delete(load);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        private void FillDataLists(PersonalLoad load = null)
        {
            if (load == null)
            {
                ViewData["Teachers"] = new SelectList(unitOfWork.Teachers.GetAll(), "TeacherID", "FullName");                
                ViewData["Classes"] = new SelectList(unitOfWork.IndividualStudies.GetAll(), "IndividualClassID", "IndividualClassName");
            }
            else
            {
                ViewData["Teachers"] = new SelectList(unitOfWork.Teachers.GetAll(), "TeacherID", "FullName",load.TeacherID);
                ViewData["Classes"] = new SelectList(unitOfWork.IndividualStudies.GetAll(), "IndividualClassID", "IndividualClassName", load.IndividualClassID);
            }
        }
    }
}