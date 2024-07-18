using Microsoft.AspNetCore.Mvc;
using Gold.Core.ServiceContracts;
using Gold.Core.DTO.AccountDTO;
using Gold.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Gold.UI.Controllers.AdminControllers
{
    [Authorize]
    public class ManageUsersController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly IRoleServices _roleServices;
        public ManageUsersController(IAccountServices accountServices, IRoleServices roleServices)
        {
            _accountServices = accountServices;
            _roleServices = roleServices;
        }
        #region Users List By Admin
        [HttpGet]
        [Route("/userslist")]
        public async Task<IActionResult> UsersList()
        {
            List<ShowUserDTO> users = await _accountServices.GetAllUsers();
            return View(users);
        }
        #endregion

        #region Edit User By Admin
        [HttpGet]
        [Route("/useredit")]

        public async Task<IActionResult> EditUser(string uName)
        {
            ApplicationUser user = await _accountServices.GetUserByUserName(uName);
            if (user == null)
                return NotFound();
            var model = new EditUserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber
            };
            ViewBag.Roles = await _roleServices.GetAllRoles();
            ViewBag.UserRoles = await _roleServices.GetUserRoles(user);
            return View(model);
        }

        [HttpPost]
        [Route("/useredit")]

        public async Task<IActionResult> EditUser(EditUserDTO edtUserDto, List<string> selectedRoles)
        {
            if (!ModelState.IsValid)
                return View(edtUserDto);
            var result = await _accountServices.EditUser(edtUserDto, selectedRoles);
            if (result)
            {
                return RedirectToAction(nameof(ManageUsersController.UsersList), "ManageUsers");
            }
            return View(edtUserDto);
        }
        #endregion

        #region Delete User By Admin
        [HttpPost]
        [Route("/deleteuser")]

        public async Task<IActionResult> DeleteUser(string uName)
        {
            var result = await _accountServices.DeleteUserByUserName(uName);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError(String.Empty, error.Description);
                return RedirectToAction(nameof(ManageUsersController.UsersList), "ManageUsers", ModelState);
            }
            return RedirectToAction(nameof(ManageUsersController.UsersList), "ManageUsers");
        }
        #endregion

        #region Add User By Admin

        [HttpGet]
        [Route("/adduser")]
        public async Task<IActionResult> AddUser()
        {
            ViewBag.Roles = await _roleServices.GetAllRoles();

            return View();
        }

        [HttpPost]
        [Route("/adduser")]
        public async Task<IActionResult> AddUser(RegisterUserDTO regUserDto, List<string> selectedRoles)
        {
            var result = await _accountServices.AddUser(regUserDto);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError(String.Empty, error.Description);
                return View(regUserDto);
            }
            var user = await _accountServices.GetUserByUserName(regUserDto.UserName);
            var result2 = await _accountServices.SetRolesToUser(user, selectedRoles);
            if (!result2.Succeeded)
            {
                foreach (IdentityError error in result2.Errors)
                    ModelState.AddModelError(String.Empty, error.Description);
                return View(regUserDto);
            }
            return RedirectToAction(nameof(ManageUsersController.UsersList), "ManageUsers");

        }
        #endregion
    }
}
