using Gold.Core.DTO.Finance;
using Gold.Core.ServiceContracts;
using Gold.Infrastructure.GoldDbContext;
using Microsoft.AspNetCore.Mvc;
using Gold.Core.Domain.Entities.Finance;
using System.Configuration;
using Gold.Core.Domain.IdentityEntities;
using System.Text.Json.Nodes;
using System.Threading;

namespace Gold.UI.Controllers
{
    public class GoldController : Controller
    {
        #region Injections
        private readonly ApplicationDbContext _context;
        private readonly IGoldServices _goldServices;
        private readonly IAccountServices _accountServices;

        public GoldController(ApplicationDbContext context, IGoldServices goldServices, IAccountServices accountServices)
        {
            _context = context;
            _goldServices = goldServices;
            _accountServices = accountServices;
        }
        #endregion

        #region User Buy Gold
        [HttpGet]
        [Route("/gold/buy")]
        public IActionResult Buy()
        {
            return View();
        }
        [HttpPost]
        [Route("/gold/buy")]
        public async Task<IActionResult> Buy(UserCashToGoldDTO uC2GDto)
        {
            if (!ModelState.IsValid)
                return View(uC2GDto);
            //send to bank and after pay set ispayed true then..
            var result = await _goldServices.SetUserCashToGold(uC2GDto,HttpContext);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "متاسفانه سفارش شما ثبت نشد");
                return View(uC2GDto);
            }

            return RedirectToAction(nameof(GoldController.UserOrdersList), "Gold");
        }
        #endregion

        #region User Sell Gold
        [HttpGet]
        [Route("/gold/sell")]
        public IActionResult Sell()
        {
            return View();
        }

        [HttpPost]
        [Route("/gold/sell")]
        public async Task<IActionResult> Sell(UserGoldToCashDTO uG2CDto)
        {
            if (!ModelState.IsValid)
                return View(uG2CDto);
            var result = await _goldServices.SetUserGoldToCash(uG2CDto, HttpContext);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "متاسفانه سفارش شما ثبت نشد");
                return View(uG2CDto);
            }

            return RedirectToAction(nameof(GoldController.UserOrdersList), "Gold");
        }
        #endregion

        #region User Gold Lists
        [Route("/userorderslist")]
        public async Task<IActionResult> UserOrdersList()        
        {
            List<UserBillDTO> bills = await _goldServices.GetUserBills(HttpContext);
            return View(bills);
        }
        #endregion

        #region Logics
        
        [Route("/executesyssellbill")]
        public async Task<string> ExecuteSysSellbill(string id)
        {
            var result = await _goldServices.ExecuteSysSellbill(id);
            return result;
        }

        [Route("/executesysbuybill")]
        public async Task<string> ExecuteSysBuybill(string id)
        {
            var result = await _goldServices.ExecuteSysBuybill(id);
           return result;
        }


        [Route("/executeusersellbill")]
        public async Task<string> ExecuteUserSellBill(string id)
        {
            var result = await _goldServices.ExecuteUserSellBill(id);
            return result;
        }

        [Route("/executeuserbuybill")]
        public async Task<string> ExecuteUserBuyBill(string id)
        {
            var result = await _goldServices.ExecuteUserBuyBill(id);
            return result;
        }
        #endregion

        #region Remote Validators

        [Route("/gold/user/CashAmountValidator")]
        public IActionResult CashAmountValidator(uint CashAmount)
        {
            if (1000000000 < CashAmount)
                return Json("مبلغ مورد نظر بایستی کمتر از یک میلیارد تومان باشد");
            if (CashAmount < 5000)
                return Json("مبلغ مورد نظر بایستی بیشتر از 5 هزار تومان باشد");
            return Json(true);
        }

        [Route("/gold/user/GoldAmountValidator")]
        public async Task<IActionResult> GoldAmountValidator(double GoldAmount)
        {
            var userGoldAmount = await _goldServices.GetUserAssets(HttpContext);
            if (10 < GoldAmount)
                return Json("سقف نقد شوندگی طلا روزانه 10 گرم میباشد");
            if (GoldAmount < 0.0001)
                return Json("حداقل نقد شوندگی طلا یک ده هزارم گرم میباشد");
            if (userGoldAmount < GoldAmount)
                return Json("این میزان موجودی طلا ندارید");
            return Json(true);
        }


        #endregion
    }
}
