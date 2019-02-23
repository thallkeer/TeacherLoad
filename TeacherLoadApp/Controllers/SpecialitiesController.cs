using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;

namespace TeacherLoadApp.Controllers
{
    public class SpecialitiesController : Controller
    {
        private UnitOfWork unitOfWork;

        public SpecialitiesController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        // GET: Specialities
        public ActionResult Index()
        {
            var specialities = unitOfWork.Specialities.Get(orderBy: q => q.OrderBy(s => s.Code));
            return View(specialities);
        }

        // GET: Specialities/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Specialities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Specialities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Specialities/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Specialities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Specialities/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Specialities/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}