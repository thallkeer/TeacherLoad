using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using DevExpress.XtraReports.UI;


namespace TeacherLoadApp
{
    public class CustomReportStorageWebExtension : DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension
    {
        public override void SetData(XtraReport report, string url)
        {
            base.SetData(report, url);
        }
        public override byte[] GetData(string url)
        {
            if (url == "SummaryReport")
            {
                var assembly = typeof(CustomReportStorageWebExtension).GetTypeInfo().Assembly;
                UnmanagedMemoryStream resource = assembly.GetManifestResourceStream("TeacherLoadApp.Views.Home.DepartmentSummaryReport.repx") as UnmanagedMemoryStream;
                MemoryStream ms = new MemoryStream();
                resource.CopyTo(ms);
                return ms.ToArray();
            }
            return base.GetData(url);
        }
    }
}