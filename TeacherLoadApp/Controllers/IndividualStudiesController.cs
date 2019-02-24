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
    public class IndividualStudiesController : Controller
    {
        private UnitOfWork unitOfWork;

        public IndividualStudiesController(TeacherLoadContext context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET: IndividualStudies
        public ActionResult Index()
        {
            return View();
        }

        // GET: IndividualStudies/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IndividualStudies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IndividualStudies/Create
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

        // GET: IndividualStudies/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IndividualStudies/Edit/5
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

        // GET: IndividualStudies/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IndividualStudies/Delete/5
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