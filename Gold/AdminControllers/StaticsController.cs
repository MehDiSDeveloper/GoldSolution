using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gold.UI.AdminControllers
{
    [Authorize]
    [Route("/admin/statics")]
    public class StaticsController : Controller
    {

        public IActionResult Index()
        {


            return View();
        }
    }
}
