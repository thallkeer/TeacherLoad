using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;
using TeacherLoadApp.Models;
using System.Linq;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoadApp.Models.Groups;

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
        public IActionResult Index(int studyYear = 1)
        {
            var groups = unitOfWork.Groups.Get(g => g.StudyYear == studyYear, includeProperties: "Speciality")
                                   .OrderBy(g => g.GroupNumber);
            var groupedGroups = groups.GroupBy(x => x.Speciality.SpecialityName)
                  .Select(g => new GroupingVM<Group> { Key = g.Key, Values = g.ToList() }).ToList();

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                return PartialView("GroupsListPartial", new GroupsIndexVM
                {
                    StudyGroup = groups.FirstOrDefault(),
                    GroupedStudyGroups = groupedGroups
                });
            }

            var model = new GroupsIndexVM
            {
                StudyGroup = groups.FirstOrDefault(),
                Years = new SelectList(Enumerable.Range(1, 4), studyYear),
                GroupedStudyGroups = groupedGroups
            };

            return View("GroupsList", model);
        }

        public IActionResult GroupDisciplines(string id)
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (isAjax)
            {
                if (id == null)
                    return View(new GroupDisciplinesVM()
                    {
                        Groups = GetGroupsList(id),
                        GroupDisciplines = new List<GroupDisciplinesVMItem>()
                    });
                return PartialView("_GroupDisciplines", GetModels(id).GroupDisciplines);
            }
            return View("GroupDisciplines", GetModels(id));
        }        

        private SelectList GetGroupsList(string selected = null)
        {
            return new SelectList(unitOfWork.Groups.Get(), "GroupNumber", "GroupNumber", selected);
        }
        

        private GroupDisciplinesVM GetModels(string id)
        {
            var items = unitOfWork.GroupLoads.GetDisciplinesByGroup(id);
            var model = new GroupDisciplinesVM();
            var modelItems = new List<GroupDisciplinesVMItem>();
            model.GroupDisciplines = modelItems;
            if (items.Any())
            {
                model.Groups = new SelectList(unitOfWork.Groups.Get(g => g.StudyYear == items.First().StudyYear),
                    "GroupNumber", "GroupNumber", id);
            }
            else
            {
                model.Groups = GetGroupsList();
            }
            foreach (GroupLoad groupLoad in items)
            {
                GroupDisciplinesVMItem item = new GroupDisciplinesVMItem
                {
                    ClassType = groupLoad.GroupStudies.GroupClassName,
                    Discipline = groupLoad.Discipline.DisciplineName,
                    TeacherName = groupLoad.Teacher.FullName
                };
                modelItems.Add(item);
            }
            return model;
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            var model = new StudyGroupVM
            {                
                Specialities = GetSpecialitiesList(),
                Group = new Group()
            };           
            return View("CreateGroup",model);
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudyGroupVM groupVM)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Groups.Insert(groupVM.Group);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            groupVM.Specialities = GetSpecialitiesList(groupVM.Group.SpecialityCode);
            return View("CreateGroup",groupVM);
        }

        // GET: Groups/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = unitOfWork.Groups.GetByID(id);
            if (group == null)
            {
                return NotFound();
            }
            var model = new StudyGroupVM
            {
                Group = group,
                Specialities = GetSpecialitiesList(group.SpecialityCode)
            };
            return View("EditGroup", model);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudyGroupVM groupVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.Groups.Update(groupVM.Group);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(groupVM.Group.GroupNumber))
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
            var model = new StudyGroupVM
            {
                Group = groupVM.Group,
                Specialities = GetSpecialitiesList(groupVM.Group.SpecialityCode)
            };
            return View("EditGroup",model);
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
            var group = unitOfWork.Groups.GetByID(id);           
            unitOfWork.Groups.Delete(group);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }        

        private SelectList GetSpecialitiesList(string selected = null)
        {
            return new SelectList(unitOfWork.Specialities.Get(), "Code", "NameWithCode", selected);
        }

        private bool GroupExists(string id)
        {
            return unitOfWork.Groups.GetByID(id) != null;
        }
    }
}
