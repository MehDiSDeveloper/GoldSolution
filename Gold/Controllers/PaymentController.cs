using Gold.Core.Domain.Entities.Assets;
using Gold.Core.Domain.Entities.Finance;
using Gold.Core.DTO.PayDTO;
using Gold.Core.Domain.IdentityEntities;
using Gold.Core.DTO.AccountDTO;
using Gold.Core.DTO.Finance;
using Gold.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using System.Diagnostics;

namespace Gold.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountServices _accountServices;
        private readonly IPayServices _payServices;

        public PaymentController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAccountServices accountServices, IPayServices payServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountServices = accountServices;
            _payServices = payServices;
        }

        #region payment
        [HttpGet]
        [Route("/PayPage/{billId}")]
        public async Task<IActionResult> PayPage(string billId)
        {
            ViewBag.HasError = false;
            if (HttpContext.User.Identity.Name == null)
            {
                ModelState.AddModelError(string.Empty, "ابتدا بایستی وارد اکانت خود بشوید");
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                ViewBag.HasError = true;
                return View();
            }
            ApplicationUser? user = await _accountServices.GetUserByUserName(HttpContext.User.Identity.Name);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "خطا در تشخیص کاربر");
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                ViewBag.HasError = true;
                return View();
            }

            UserCashToGold bill = await _payServices.getBuyBillbyId(billId);
            if (bill == null)
            {
                ModelState.AddModelError(string.Empty, "خطا در تشخیص فاکتور");
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                ViewBag.HasError = true;
                return View();
            }

            if (bill.IsPayed == true)
            {
                ModelState.AddModelError(string.Empty, "فاکتور مورد نظر پیش از این پرداخت شده");
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                ViewBag.HasError = true;
                return View();
            }

            UserGoldAsset asset = await _payServices.getUserAssetByUser(user);

            PayBillDTO payBillDto = new()
            {
                Id = bill.Id,
                DealType = "deposit",
                IsPayed = bill.IsPayed,
                ToAppUser = user,
                AssetId = asset.Id,
                TotalCashAmount = bill.TotalCash,
                BillRegDateTime = bill.DateTime,
                ExecutionTime = null,
                UserCashAsset = asset.TotalCashAsset,
                FinalPayment = 0,
                DealName = "خرید طلا",
            };

            if (asset.TotalCashAsset > 0 && asset.TotalCashAsset >= bill.TotalCash)
            {
                payBillDto.CashToPay = 0;
                ViewBag.wallet = "full";
            }
            if (asset.TotalCashAsset > 0 && asset.TotalCashAsset < bill.TotalCash)
            {
                payBillDto.CashToPay = (int)(bill.TotalCash - asset.TotalCashAsset);
                ViewBag.wallet = "part";
            }
            if (asset.TotalCashAsset == 0)
            {
                payBillDto.CashToPay = (int)bill.TotalCash;
                ViewBag.wallet = "none";
            }

            return View(payBillDto);
        }



        [HttpPost]
        [Route("/Payment")]
        public async Task<IActionResult> Payment(PayBillDTO payBillDto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("PayPage", "Payment", payBillDto.Id);
            }
            if(payBillDto.PayType=="wallet")
            {
                await _payServices.withdrawFromAssetByIdandAmount(payBillDto.AssetId, payBillDto.FinalPayment);
                await _payServices.setUserCashGoldIsPayedById(payBillDto.Id);
                return RedirectToAction("UserOrdersList", "Gold");
            }
            var payment = new ZarinpalSandbox.Payment((int)payBillDto.FinalPayment);
            string transId = Guid.NewGuid().ToString();
            var response = payment.PaymentRequest("خرید طلا", "http://localhost:5046/ReturnFromBank/" + transId, "mahdiii.montazeri@gmail.com", "09125850371");
            if (response.Result.Status == 100)
            {
                await _payServices.setTransaction(payBillDto, transId);
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + response.Result.Authority);
            }
            return View();
        }

        [Route("/returnFromBank/{transId}")]
        public async Task<IActionResult> ReturnFromBank(string transId)
        {
            if (HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" && HttpContext.Request.Query["Authority"] != "")
            {
                Transaction trans = await _payServices.getTransactionById(transId);

                string authority = HttpContext.Request.Query["Authority"];
                var payment = new ZarinpalSandbox.Payment((int)trans.Cash);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    await _payServices.setTransactionIsPayedById(transId);
                    ViewBag.code = res.RefId;
                    ViewBag.success = true;
                    ViewBag.payWay = trans.PayType;
                    if (trans.PayType == "bank")
                    {
                        await _payServices.setUserCashGoldIsPayedById(trans.PayBillId);
                    }
                    else if (trans.PayType == "bankwallet")
                    {
                        await _payServices.setUserAssetCashToZeroById(trans.AssetId);
                        await _payServices.setUserCashGoldIsPayedById(trans.PayBillId);
                    }
                }
                TransactionDTO transDto = new TransactionDTO()
                {
                    Cash = trans.Cash,
                    DateTime = trans.DateTime,
                    DealName = trans.DealName,
                    DealType = trans.DealType,
                    ExecutionTime = trans.ExecutionTime,
                    IsPayed = trans.IsPayed,
                    PayWay = trans.PayType,
                    AssetCash = trans.AssetCash,
                    PayBillId=trans.PayBillId,
                };
                return View(transDto);
            }
            return View();
        }

        #endregion
    }
}
