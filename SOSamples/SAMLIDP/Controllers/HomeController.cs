using Microsoft.AspNetCore.Mvc;
using SAMLIDP.Models;
using System.Diagnostics;
using System.DirectoryServices;


namespace SAMLIDP.Controllers
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

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (loginModel != null)
            {
                // Verify username and password

            }

            return View();
        }

        [HttpPost]
        public IActionResult LoginUsingDSONE()
        {
            using (DirectoryEntry de = new DirectoryEntry("LDAP://MyDomainController"))
            {
                using (DirectorySearcher adSearch = new DirectorySearcher(de))
                {
                    adSearch.Filter = "";
                    SearchResult adSearchResult = adSearch.FindOne();
                }
            }

            return Redirect("http://www.google.com");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}