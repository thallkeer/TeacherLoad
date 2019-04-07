using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public FileResult CreateReport()
        {
            ReportDocument rd = new ReportDocument();
            
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "SummaryReport.rpt"));          

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            
            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
           
            return File(stream, System.Net.Mime.MediaTypeNames.Application.Pdf, "Summary.pdf");
        }
    }
}
