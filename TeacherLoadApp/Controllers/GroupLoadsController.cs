using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;
using TeacherLoadApp.Models;
using TeacherLoadApp.Helpers;
using static TeacherLoad.Core.Models.GroupLoad;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TeacherLoadApp.Controllers
{
    public class GroupLoadsController : Controller
    {
        private IUnitOfWork unitOfWork;
        public GroupLoadsController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        public IActionResult Index()
        {
            //var loads = unitOfWork.GroupLoads.GetAll();    
            //return View("GroupLoadsList",loads);
            return RedirectToAction("TeachersGroupLoad");
        }

        public ActionResult TeachersGroupLoad(int teacherID)
        {
            List<TeachersListViewModel> teachers = new List<TeachersListViewModel>();            
            foreach (Teacher teacher in unitOfWork.Teachers.Get())
            {
                teachers.Add(new TeachersListViewModel()
                {
                    TeacherID = teacher.TeacherID,
                    TeacherName = teacher.FullName
                });
            }

            if (teacherID == 0)
                teacherID = teachers.First().TeacherID;

            SelectList teachersList = new SelectList(teachers, "TeacherID", "TeacherName", teacherID);

            var loadsByTeacher = unitOfWork.GroupLoads.GetByTeacher(teacherID);

            List<GroupingViewModel<GroupLoad>> groupedLoads = loadsByTeacher
                .GroupBy(x => new { x.DisciplineID, x.GroupStudiesID })
                .Select(g => g.First()).GroupBy(x => x.Discipline.DisciplineName)
                .Select(g => new GroupingViewModel<GroupLoad> { Key = g.Key, Values = g.ToList() }).ToList();
                                             
            var groupStudiesList = new SelectList(unitOfWork.GroupStudies.Get(), "GroupClassID", "GroupClassName");

            List<int> years = loadsByTeacher.Select(x => x.StudyYear).Distinct().ToList();
            List<SelectListItem> yearsList = new List<SelectListItem>();          
            years.ForEach(x => yearsList.Add(new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString(),
            }));           

            var model = new TeacherLoadViewModel()
            {
                TeachersList = teachers,
                GroupStudies = groupStudiesList,
                SelectedTeacher = teacherID,
                GroupedLoads = groupedLoads,
                Semesters = GetEnumSelectList(typeof(SemesterType)),
                StudyTypes = GetEnumSelectList(typeof(StudyTypes)),
                StudyYears = new SelectList(yearsList,"Value","Text")
            };

            return View(model);
        }

        private static SelectList GetEnumSelectList(Type type, int selected = 0)
        {
            Array values = Enum.GetValues(type);
            List<SelectListItem> items = new List<SelectListItem>(values.Length);
            foreach (var i in values)
            {
                items.Add(new SelectListItem
                {
                    Text = i.GetType()
                            .GetMember(i.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName(),//Enum.GetName(type, i),
                    Value = ((int)i).ToString(),
                   
                    Selected = (int)i == selected
                });
            }           
            return new SelectList(items,"Value","Text");
        }

        public ActionResult GetGroupLoads(int teacherID, int groupClassID, int semester, int studyType, int studyYear)
        {
            var filter = BuildFilter(teacherID,groupClassID,semester,studyType,studyYear);            

            IEnumerable<GroupLoad> loads = unitOfWork.GroupLoads
                .Get(filter,includeProperties:"Teacher,Discipline,GroupStudies")         
                .GroupBy(l => new { l.DisciplineID, l.GroupStudiesID })
                .Select(g => g.First());            
            
            List<GroupingViewModel<GroupLoad>> groupedLoads = loads.GroupBy(x => x.Discipline.DisciplineName)
               .Select(g => new GroupingViewModel<GroupLoad> { Key = g.Key, Values = g.ToList() }).ToList();

            return PartialView("TeacherLoadsPartial",groupedLoads);
        }

        private Expression<Func<GroupLoad, bool>> BuildFilter(int teacherID,int groupStudyID=0,int semester=0,int studyType=0,int studyYear=0)
        {           
            Expression<Func<GroupLoad, bool>> filter = (x) => x.TeacherID == teacherID;            
            if (groupStudyID != 0)
                filter = filter.And((x) => x.GroupStudiesID == groupStudyID);
            if (semester != 0)
                filter = filter.And((x) => (int)x.Semester == semester);
            if (studyType != 0)
                filter = filter.And((x) => (int)x.StudyType == studyType);
            if (studyYear != 0)
                filter = filter.And((x) => x.StudyYear == studyYear);
            return filter;
        }

        // GET: GroupLoads/Create
        public ActionResult Create()
        {
            FillDataLists();
            return View("CreateGroupLoad");
        }

        // POST: GroupLoads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupLoad load)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.GroupLoads.Insert(load);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            FillDataLists(load);
            return View("CreateGroupLoad",load);
        }

        // GET: GroupLoads/Edit/5
        public ActionResult Edit(GroupLoad item)
        {
            var load = unitOfWork.GroupLoads.GetByID(item);
            if (load == null)
            {
                return NotFound();
            }
            FillDataLists(load);
            return View("EditGroupLoad",load);
        }

        // POST: GroupLoads/Edit/5
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(GroupLoad load)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.GroupLoads.Update(load);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View("EditGroupLoad",load);
        }

        // GET: GroupLoads/Delete/5
        public ActionResult Delete(int id)
        {
            var load = unitOfWork.GroupLoads.GetAll();
            return View("DeleteGroupLoad");
        }

        // POST: GroupLoads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var load = unitOfWork.GroupLoads.GetByID(id);
            unitOfWork.GroupLoads.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        private void FillDataLists(GroupLoad load = null)
        {
            if (load == null)
            {
                ViewData["Teachers"] = new SelectList(unitOfWork.Teachers.GetAll(), "TeacherID", "FullName");
                ViewData["Groups"] = new SelectList(unitOfWork.Groups.GetAll(), "GroupNumber", "GroupNumber");
                ViewData["Disciplines"] = new SelectList(unitOfWork.Disciplines.GetAll(), "DisciplineID", "DisciplineName");
                ViewData["GroupStudies"] = new SelectList(unitOfWork.GroupStudies.GetAll(), "GroupClassID", "GroupClassName");
            }
            else
            {
                ViewData["TeacherID"] = new SelectList(unitOfWork.Teachers.GetAll(), "TeacherID", "FullName", load.TeacherID);
                ViewData["GroupStudiesID"] = new SelectList(unitOfWork.GroupStudies.GetAll(), "GroupClassID", "GroupClassName", load.GroupStudiesID);
                ViewData["GroupNumber"] = new SelectList(unitOfWork.Groups.GetAll(), "GroupNumber", "GroupNumber", load.GroupNumber);
                ViewData["DisciplineID"] = new SelectList(unitOfWork.Disciplines.GetAll(), "DisciplineID", "DisciplineName", load.DisciplineID);
            }
        }
    }
}