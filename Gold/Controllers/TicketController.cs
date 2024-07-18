using Gold.Core.Domain.IdentityEntities;
using Gold.Core.DTO.TicketDTO;
using Gold.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gold.UI.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ITicketServices _ticketServices;
        private readonly IAccountServices _accountServices;
        public TicketController(ITicketServices ticketServices, IAccountServices accountServices)
        {
            _ticketServices = ticketServices;
            _accountServices = accountServices;

        }
        public IActionResult Index()
        {
            TicketMessageDTO message = new();
            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TicketMessageDTO message)
        {
            var user = await _accountServices.GetLogedinUser(HttpContext);
            message = await _ticketServices.SetTicketMessage(message, user);

            return View(message);
        }
    }


}
