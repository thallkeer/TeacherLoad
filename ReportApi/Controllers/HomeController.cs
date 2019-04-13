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

            Sections ReportSections = rd.ReportDefinition.Sections;

            ReportObjects crReportObjects;
            SubreportObject crSubreportObject;
            ReportDocument crSubreportDocument;
            Database crDatabase;
            Tables crTables;
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ServerName = "DESKTOP-UR40SF3";
            connectionInfo.DatabaseName = "DepartmentLoad";
            connectionInfo.UserID = "sa";
            connectionInfo.Password = "123456";

            foreach (Section section in ReportSections)
            {
                crReportObjects = section.ReportObjects;

                foreach (ReportObject crReportObject in crReportObjects)
                {
                    if (crReportObject.Kind != ReportObjectKind.SubreportObject)
                        continue;

                    crSubreportObject = (SubreportObject)crReportObject;
                    crSubreportDocument = crSubreportObject.OpenSubreport(crSubreportObject.SubreportName);
                    crDatabase = crSubreportDocument.Database;
                    crTables = crDatabase.Tables;

                    foreach (Table crTable in crTables)
                    {
                        TableLogOnInfo crTableLogOnInfo = crTable.LogOnInfo;
                        crTableLogOnInfo.ConnectionInfo = connectionInfo;
                        crTable.ApplyLogOnInfo(crTableLogOnInfo);
                    }
                }
            }

            Tables tables = rd.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }


            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            
            Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
           
            return File(stream, System.Net.Mime.MediaTypeNames.Application.Pdf, "Summary.pdf");
        }
    }
}
