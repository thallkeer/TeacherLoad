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
        private readonly IUnitOfWork unitOfWork;
        public GroupLoadsController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
            //context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
        }

        public IActionResult Index()
        {
            return RedirectToAction("TeachersGroupLoad");
        }        

        [HttpGet]
        public ActionResult TeachersGroupLoad(int teacherID, int groupClassID, int semester, int studyType, int studyYear)
        {
            var allTeachers = unitOfWork.Teachers.Get(orderBy: q => q.OrderBy(t => t.LastName));
            if (teacherID == 0)
            {
                teacherID = allTeachers.First().TeacherID;
            }
            var teachersList = new SelectList(allTeachers, "TeacherID", "FullName",teacherID);

            var groupedLoads = GetGroupedLoads(teacherID, groupClassID, semester, studyType, studyYear);                      
                                             
            var groupStudiesList = new SelectList(unitOfWork.GroupStudies.Get(), "GroupClassID", "GroupClassName",groupClassID);

            var model = new TeacherGroupLoadVM
            {
                Teachers = teachersList,
                GroupStudies = groupStudiesList,
                GroupedLoads = groupedLoads.ToList(),
                Semesters = GetEnumSelectList(typeof(SemesterType), semester),
                StudyTypes = GetEnumSelectList(typeof(StudyTypes), studyType),
                StudyYears = new SelectList(Enumerable.Range(1,4), studyYear)
            };

            return View(model);
        }           

        [HttpGet]
        public ActionResult GetGroupLoads(int teacherID, int groupClassID, int semester, int studyType, int studyYear)
        {
            var groupedLoads = GetGroupedLoads(teacherID,groupClassID,semester,studyType,studyYear);
            return PartialView("_TeacherLoads",groupedLoads);
        }      

        // GET: GroupLoads/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("CreateGroupLoad",BuildModel());
        }

        // POST: GroupLoads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupLoadVM load)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.GroupLoads.Insert(load.GroupLoad);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }            
            return View("CreateGroupLoad", BuildModel(load.GroupLoad));
        }

        // GET: GroupLoads/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var load = unitOfWork.GroupLoads.GetByID(id);
            if (load == null)
            {
                return NotFound();
            }
            var model = BuildModel(load);            
            return View("EditGroupLoad",model);
        }       

        // POST: GroupLoads/Edit/5
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(GroupLoadVM load)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    unitOfWork.GroupLoads.Update(load.GroupLoad);
                    unitOfWork.Save();                   
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View("EditGroupLoad",BuildModel(load.GroupLoad));
        }

        // GET: GroupLoads/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var load = unitOfWork.GroupLoads.GetByID(id);
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

        private IEnumerable<GroupingVM<GroupLoad>> GetGroupedLoads(int teacherID, int groupClassID, int semester, int studyType, int studyYear)
        {
            var filter = BuildFilter(teacherID, groupClassID, semester, studyType, studyYear);

            IEnumerable<GroupLoad> loads = unitOfWork.GroupLoads.GetGroupedLoadsByFilter(filter);

            return loads.GroupBy(x => x.Discipline.DisciplineName)
               .Select(g => new GroupingVM<GroupLoad> { Key = g.Key, Values = g.ToList() });
        }

        public ActionResult GetGroupsByCourse(int studyYear)
        {
            var groups = new SelectList(unitOfWork.Groups.Get(g => g.StudyYear == studyYear), "GroupNumber", "GroupNumber");
            return PartialView("_GetGroups",new GroupLoadVM { Groups = groups});
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
                            .GetName(),
                    Value = ((int)i).ToString()
                });
            }
            return new SelectList(items, "Value", "Text", selected);
        }

        private static Expression<Func<GroupLoad, bool>> BuildFilter(int teacherID, int groupStudyID, int semester, int studyType, int studyYear)
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

        private GroupLoadVM BuildModel(GroupLoad groupLoad = null)
        {
            if (groupLoad == null)
                groupLoad = new GroupLoad();
            var model = new GroupLoadVM
            {
                GroupLoad = groupLoad,
                Teachers = new SelectList(unitOfWork.Teachers.GetAllForSelectList(), "TeacherID", "FullName", groupLoad.TeacherID),
                Disciplines = new SelectList(unitOfWork.Disciplines.GetAll(), "DisciplineID", "DisciplineName", groupLoad.DisciplineID),
                Groups = new SelectList(unitOfWork.Groups.Get(g => g.StudyYear == groupLoad.StudyYear), "GroupNumber", "GroupNumber", groupLoad.GroupNumber),
                GroupStudies = new SelectList(unitOfWork.GroupStudies.GetAll(), "GroupClassID", "GroupClassName", groupLoad.GroupStudiesID),
                Semesters = GetEnumSelectList(typeof(SemesterType), (int)groupLoad.Semester),
                StudyTypes = GetEnumSelectList(typeof(StudyTypes), (int)groupLoad.StudyType),
                StudyYears = new SelectList(Enumerable.Range(1,4), groupLoad.StudyYear)
            };
            return model;
        }
    }
}