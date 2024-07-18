using Gold.Core.Domain.IdentityEntities;
using Gold.Core.DTO.RoleDTO;
using Gold.Core.ServiceContracts;
using Gold.Infrastructure.GoldDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Infrastructure.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _Context;
        public RoleServices(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext Context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _Context = Context;
        }

        public async Task<IdentityResult> AddRole(AddOrEditRoleDTO roleDto)
        {
            ApplicationRole role = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleDto.Name,
            };
            var result = await _roleManager.CreateAsync(role);
            return result;
        }

        public async Task<IdentityResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var result = await _roleManager.DeleteAsync(role);
            return result;
        }

        public async Task<IdentityResult> EditRole(AddOrEditRoleDTO roleDto)
        {
            ApplicationRole role = await _roleManager.FindByIdAsync(roleDto.Id);
            role.Name = roleDto.Name;

            var result = await _roleManager.UpdateAsync(role);
            return result;
        }

        public async Task<IEnumerable<ShowRoleDTO>> GetAllRoles()
        {
            return await _roleManager.Roles.Select(r => new ShowRoleDTO
            {
                Id = r.Id,
                Name = r.Name,
            }).ToListAsync();
        }

        public async Task<AddOrEditRoleDTO> GetRoleDtoById(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            AddOrEditRoleDTO rolDto = new()
            {
                Name = role.Name,
                Id = role.Id,
            };
            return rolDto;
        }

        public async Task<IEnumerable<string>> GetUserRoles(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
