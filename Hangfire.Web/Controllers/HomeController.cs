using Hangfire.Web.BackgroundJob;
using Hangfire.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            BackgroundJob.RecurringJob.ReportingJob();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SignUp()
        {
            //Üye kayıt işlemi gerçekleşir bu metotda
            //yeni üye olan kullanıcını userId sini al
            FireAndForgetJob.EmailSendJobToUser("1234", "Sitemizde hoşgeldiniz");//jobımıza verileri alıp burada gönderiyoruz
            return View();
        }
    }
}
