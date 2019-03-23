using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;
using TeacherLoadApp.Models;
using System.Linq;
using TeacherLoad.Core.DataInterfaces;

namespace TeacherLoadApp.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public GroupsController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET: Groups
        public IActionResult Index(int year=1)
        {            
            var groups = unitOfWork.Groups.Get(g => g.StudyYear == year, includeProperties: "Speciality")
                                   .OrderBy(g => g.GroupNumber);
            var groupedGroups = groups.GroupBy(x => x.Speciality.SpecialityName)
                  .Select(g => new GroupingVM<Group> { Key = g.Key, Values = g.ToList() }).ToList();

            var model = new GroupsIndexVM
            {
                Instance = groups.FirstOrDefault(),
                Years = new SelectList(unitOfWork.Groups.Get().Select(g => g.StudyYear).Distinct(), year),
                GroupedStudyGroups = groupedGroups
            };

            return View("GroupsList", model);
        }

        public IActionResult GroupDisciplines(string id)
        {
            var groupselect = new SelectList(unitOfWork.Groups.Get(), "GroupNumber", "GroupNumber", id);
            ViewBag.Groups = groupselect;
            if (id == null)
                return View(new List<GroupDisciplinesVM>());
            return PartialView("GroupDisciplinesPartial", GetModels(id));
        }

        public IActionResult GroupDisciplinesPost(string id)
        {                     
            var groupselect = new SelectList(unitOfWork.Groups.Get(), "GroupNumber", "GroupNumber", id);
            ViewBag.Groups = groupselect;
            return View("GroupDisciplines", GetModels(id));
        }

        private List<GroupDisciplinesVM> GetModels(string id)
        {
            var items = unitOfWork.GroupLoads.GetDisciplinesByGroup(id);
            List<GroupDisciplinesVM> models = new List<GroupDisciplinesVM>();
            foreach (GroupLoad groupLoad in items)
            {
                GroupDisciplinesVM model = new GroupDisciplinesVM
                {
                    ClassType = groupLoad.GroupStudies.GroupClassName,
                    Discipline = groupLoad.Discipline.DisciplineName,
                    TeacherName = groupLoad.Teacher.FullName
                };
                models.Add(model);
            }
            return models;
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            ViewData["SpecialityCode"] = new SelectList(unitOfWork.Specialities.Get(), "Code", "SpecialityName");
            return View("CreateGroup");
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GroupNumber,FullTime,Semester,SpecialityCode")] Group @group)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Groups.Insert(@group);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecialityCode"] = new SelectList(unitOfWork.Specialities.Get(), "Code", "SpecialityName", @group.SpecialityCode);
            return View("CreateGroup",@group);
        }

        // GET: Groups/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = unitOfWork.Groups.GetByID(id);
            if (@group == null)
            {
                return NotFound();
            }
            ViewData["SpecialityCode"] = new SelectList(unitOfWork.Specialities.Get(), "Code", "SpecialityName", @group.SpecialityCode);
            return View("EditGroup",@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("GroupNumber,FullTime,Semester,SpecialityCode")] Group @group)
        {
            if (id != @group.GroupNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.Groups.Update(@group);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.GroupNumber))
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
            ViewData["SpecialityCode"] = new SelectList(unitOfWork.Specialities.Get(), "Code", "SpecialityName", @group.SpecialityCode);
            return View("EditGroup",@group);
        }

        // GET: Groups/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = unitOfWork.Groups.GetByID(id);
            if (@group == null)
            {
                return NotFound();
            }

            return View("DeleteGroup",@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var @group = unitOfWork.Groups.GetByID(id);
            unitOfWork.Groups.Delete(@group);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(string id)
        {
            return unitOfWork.Groups.GetByID(id) != null;
        }
    }
}
