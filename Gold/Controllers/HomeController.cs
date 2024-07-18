using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gold.UI.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        #region Errors
        [Route("/Error/404")]
        public IActionResult Error404()
        {
            return View("Views/ErrorPages/404page.cshtml");
        }
        #endregion

    }
}
