using Microsoft.AspNetCore.Mvc;
using Gold.Core.ServiceContracts;
using Gold.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Gold.Core.DTO.RoleDTO;
using Microsoft.AspNetCore.Authorization;

namespace Gold.UI.Controllers.AdminControllers
{
    [Authorize]
    public class ManageRolesController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly IRoleServices _roleServices;
        public ManageRolesController(IAccountServices accountServices, IRoleServices roleServices)
        {
            _accountServices = accountServices;
            _roleServices = roleServices;
        }

        [HttpGet]
        [Route("/roleslist")]
        public async Task<IActionResult> RolesList()
        {
            IEnumerable<ShowRoleDTO> roles = await _roleServices.GetAllRoles();
            return View(roles);
        }


        #region Add Role 
        [HttpGet]
        [Route("/addrole")]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        [Route("/addrole")]
        public async Task<IActionResult> AddRole(AddOrEditRoleDTO rolDto)
        {
            if (!ModelState.IsValid || rolDto == null)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(rolDto);
            }
            var result = await _roleServices.AddRole(rolDto);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ManageRolesController.RolesList), "ManageRoles");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError(String.Empty, error.Description);
                return View(rolDto);
            }
        }
        #endregion


        #region Edit Role
        [HttpGet]
        [Route("/editrole")]
        public async Task<IActionResult> EditRole(string id)
        {
            var roleDto = await _roleServices.GetRoleDtoById(id);
            return View(roleDto);
        }
        [HttpPost]
        [Route("/editrole")]
        public async Task<IActionResult> EditRole(AddOrEditRoleDTO editRoleDto)
        {
            if (!ModelState.IsValid || editRoleDto == null)
            {
                ModelState.AddModelError(string.Empty, "متاسفانه تغییرات ثبت نشد");
                return View(editRoleDto);
            }
            var result = await _roleServices.EditRole(editRoleDto);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError(String.Empty, error.Description);
                return View(editRoleDto);
            }            
            return RedirectToAction(nameof(ManageRolesController.RolesList), "ManageRoles");
        }
        #endregion


        #region Delete Role
        [HttpPost]
        [Route("/deleterole")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _roleServices.DeleteRole(id);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                    ModelState.AddModelError(String.Empty, error.Description);
                return RedirectToAction(nameof(ManageRolesController.RolesList), "ManageRoles");
            }
            return RedirectToAction(nameof(ManageRolesController.RolesList), "ManageRoles");
        }
        #endregion

    }
}
