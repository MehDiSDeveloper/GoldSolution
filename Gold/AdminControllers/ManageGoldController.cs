using Gold.Core.DTO.AccountDTO;
using Gold.Core.DTO.Finance;
using Gold.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gold.UI.AdminControllers
{
    [Authorize]
    public class ManageGoldController : Controller
    {
        #region Injections
        private readonly IAccountServices _accountServices;
        private readonly IGoldServices _goldServices;   

        public ManageGoldController(IAccountServices accountServices,IGoldServices goldServices)
        {
            _accountServices = accountServices;
            _goldServices = goldServices;
        }
        #endregion


        #region User bills management
        [Route("/usersgolddemands")]
        public async Task<IActionResult> ListOfUsersGoldDemands()
        {
            var userGoldDemands =await _goldServices.GetusersGoldDemands();
            return View(userGoldDemands);
        }

        [Route("/usersgoldoffers")]
        public async Task<IActionResult> ListOfUserGoldOffers()
        {
            var userGoldOffers = await _goldServices.GetusersGoldOffers();
            return View(userGoldOffers);
        }
        #endregion


        #region System bills management
        [HttpGet]
        [Route("/setsystemgoldtocash")]
        public IActionResult SetSystemGoldToCash()
        {
            return View();
        }

        [HttpPost]
        [Route("/setsystemgoldtocash")]
        public async Task<IActionResult> SetSystemGoldToCash(SystemGoldToCashDTO sysGCDto)
        {
            if(!ModelState.IsValid)
                return View(sysGCDto);
            var result = await _goldServices.SetSystemGoldToCash(sysGCDto, HttpContext);
            if(result)
                return RedirectToAction(nameof(ManageGoldController.SystemGoldBillsList), "ManageGold");
            return View(sysGCDto);
        }                


        [HttpGet]
        [Route("/setsystemcashtogold")]
        public IActionResult SetSystemCashToGold()
        {
            return View();
        }

        [HttpPost]
        [Route("/setsystemcashtogold")]
        public async Task<IActionResult> SetSystemCashToGold(SystemCashToGoldDTO sysCGDto)
        {
            if (!ModelState.IsValid)
                return View(sysCGDto);
            var result = await _goldServices.SetSystemCashToGold(sysCGDto, HttpContext);
            if (result)
                return RedirectToAction(nameof(ManageGoldController.SystemGoldBillsList), "ManageGold");
            return View(sysCGDto);
        }

        

        [Route("/systemgoldbillslist")]
        public async Task<IActionResult> SystemGoldBillsList(string status, string type, string searchString)
        {
            List <SystemBillDTO> bills = await _goldServices.GetSysBills(status, type, searchString);
            ViewBag.Status = status;
            ViewBag.Type = type;
            ViewBag.SearchString = searchString;
            return View(bills);
        }

        [Route("/SystemGoldBillsListPartial")]
        public async Task<IActionResult> SystemGoldBillsListPartial(string status, string type, string searchString)
        {
            List<SystemBillDTO> bills = await _goldServices.GetSysBills(status, type, searchString);
            ViewBag.Status = status;
            ViewBag.Type = type;
            ViewBag.SearchString = searchString;
            return View("_SystemGoldBillsListPartial", bills);
        }


        [Route("/Usersgoldbillslist")]
        public async Task<IActionResult> UsersGoldBillsList(string status, string type, string searchString)
        {
            List<UserBillDTO> bills = await _goldServices.GetUsersBills(status, type, searchString);
            ViewBag.Status = status;
            ViewBag.Type = type;
            ViewBag.SearchString = searchString;
            return View(bills);
        }
        [HttpGet]
        [Route("/UsersGoldBillsListPartial")]
        public async Task<IActionResult> UsersGoldBillsListPartial(string status, string type,string searchString)
        {
            List<UserBillDTO> bills = await _goldServices.GetUsersBills(status,type,searchString);
            ViewBag.Status = status;
            ViewBag.Type = type;
            ViewBag.SearchString = searchString;
            return View("_UsersGoldBillsListPartial", bills);
        }


        [HttpPost]
        [Route("/switchsusspendbill")]
        public async Task<string> SwitchSusspendBill(string id,string billType)
        {
            var Result = "";
            if (billType == "userSell")
            {
                Result = await _goldServices.SwitchSusspendUserSellBill(id);
            }
            else if (billType == "userBuy")
            {
                Result = await _goldServices.SwitchSusspendUserBuyBill(id);
            }
            else if (billType == "sysSell")
            {
                Result = await _goldServices.SwitchSusspendSysSellBill(id);
            }
            else if (billType == "sysBuy")
            {
                Result = await _goldServices.SwitchSusspendSysBuyBill(id);
            }
            if (Result == "قبض یافت نشد")
            {
                ModelState.AddModelError(string.Empty, Result);
            }
            return Result;
        }

        #endregion       


        #region Remote Validators
        [Route("/gold/system/TotalCashValidator")]
        public IActionResult SysTotalCashValidator(uint TotalCash)
        {
            if (1000000000 < TotalCash)
                return Json("مبلغ مورد نظر بایستی کمتر از یک میلیارد تومان باشد");
            if (TotalCash < 5000)
                return Json("مبلغ مورد نظر بایستی بیشتر از 5 هزار تومان باشد");
            return Json(true);
        }
        //[Route("/gold/system/GoldUnitPriceValidator")]
        //public IActionResult SysGoldUnitPriceValidator(uint GoldUnitPrice)
        //{
        //    if (1000000000 < GoldUnitPrice)
        //        return Json("مبلغ مورد نظر بایستی کمتر از یک میلیارد تومان باشد");
        //    if (GoldUnitPrice < 5000)
        //        return Json("مبلغ مورد نظر بایستی بیشتر از 5 هزار تومان باشد");
        //    return Json(true);
        //}

        [Route("/gold/system/GoldAmountValidator")]
        public IActionResult SysGoldAmountValidator(double GoldAmount)
        {
            if (100 < GoldAmount)
                return Json("سقف نقد شوندگی طلا 100 گرم میباشد");
            if (GoldAmount < 0.0001)
                return Json("حداقل نقد شوندگی طلا یک ده هزارم گرم میباشد");
            return Json(true);
        }

        //[Route("/gold/system/TotalBoughtPriceValidator")]
        //public IActionResult SysTotalBoughtPriceValidator(uint TotalBoughtPrice)
        //{
        //    if (1000000000 < TotalBoughtPrice)
        //        return Json("مبلغ مورد نظر بایستی کمتر از یک میلیارد تومان باشد");
        //    if (TotalBoughtPrice < 5000)
        //        return Json("مبلغ مورد نظر بایستی بیشتر از 5 هزار تومان باشد");
        //    return Json(true);
        //}


        #endregion
    }
}
