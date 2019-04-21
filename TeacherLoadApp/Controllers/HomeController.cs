using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeacherLoad.Core.DataInterfaces;
using TeacherLoad.Core.Models;
using TeacherLoad.Data.Service;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;


namespace TeacherLoadApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpClientFactory clientFactory;

        public HomeController(TeacherLoadContext context, SignInManager<ApplicationUser> signInManager, IHttpClientFactory clientFactory)
        {
            unitOfWork = new UnitOfWork(context);
            _signInManager = signInManager;
            this.clientFactory = clientFactory;
        }

        public IActionResult Index()
        {           
            if (_signInManager.IsSignedIn(User))
            {
                return View();
            }
            return RedirectToAction("Login", "Users");
        }

        public IActionResult ShowReport()
        {
            return View("ReportView");           
        }     

        //public async Task<FileResult> DoGet()
        //{
            //var request = new HttpRequestMessage(HttpMethod.Get,
           //"http://localhost:52574/Home/CreateReport");
            ////request.Headers.Add("Accept", System.Net.Mime.MediaTypeNames.Application.Pdf);
            //////request.Headers.Add("Accept", "application/vnd.github.v3+json");
            ////request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            //var client = clientFactory.CreateClient();

            //var response = await client.SendAsync(request);

            ////if (response.IsSuccessStatusCode)
            ////{                
                //var fileResult = response.Content
                    //.ReadAsByteArrayAsync().Result;
            ////System.IO.File.WriteAllBytes("test.pdf", fileResult);
            //return File(fileResult, "application/pdf");
            ////ViewData["File"] = file;
            ////return View("Report");
        //}
    }
}