using Gold.Core.Domain.IdentityEntities;
using Gold.Core.DTO.AccountDTO;
using Gold.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace Gold.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountServices _accountServices;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAccountServices accountServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountServices = accountServices;
        }

        #region Register user
        [HttpGet]
        [Route("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register(RegisterUserDTO regUserDto)
        {
            if (!ModelState.IsValid || regUserDto == null)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(regUserDto);
            }

            //register user here
            var result = await _accountServices.AddUser(regUserDto);
            if (result.Succeeded)
            {
                ApplicationUser user = await _accountServices.GetUserByUserName(regUserDto.UserName);
                //sign in
                await _signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction(nameof(AccountController.UserPanel), "Account");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError(String.Empty, error.Description);
                return View(regUserDto);
            }

        }
        #endregion

        #region LogIn
        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO LoginDto)
        {
            if (!ModelState.IsValid || LoginDto == null)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(LoginDto);
            }

            //sign in user
            ApplicationUser user = await _accountServices.GetUserByPhone(LoginDto.Phone);
            await _signInManager.SignInAsync(user,isPersistent:true);
            
            return RedirectToAction(nameof(AccountController.UserPanel), "Account");
        }
        #endregion

        #region LogOut
        [HttpGet]
        [Route("/logoutview")]
        public IActionResult LogoutView()
        {
            return View();
        }

        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion

        #region User Panel
        [HttpGet]
        [Route("/userpanel")]
        public async Task<IActionResult> UserPanel()
        {
            var user =await _accountServices.GetLogedinUser(HttpContext);
            ShowUserDTO showUser = new()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
            };
            return View(showUser);
        }
        #endregion
    }
}
