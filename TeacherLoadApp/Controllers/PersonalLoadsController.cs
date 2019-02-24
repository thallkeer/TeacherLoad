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
    public class PersonalLoadsController : Controller
    {
        private UnitOfWork unitOfWork;

        public PersonalLoadsController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET: PersonalLoads
        public ActionResult Index()
        {
            var loads = unitOfWork.PersonalLoads.GetAll();
            return View(loads);
        }

        // GET: PersonalLoads/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PersonalLoads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalLoads/Create
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

        // GET: PersonalLoads/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PersonalLoads/Edit/5
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

        // GET: PersonalLoads/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonalLoads/Delete/5
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