using Gold.Core.Domain.IdentityEntities;
using Gold.Core.DTO.AccountDTO;
using Gold.Core.DTO.RoleDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.ServiceContracts
{
    public interface IRoleServices
    {
        Task<IEnumerable<ShowRoleDTO>> GetAllRoles();
        Task<IEnumerable<string>> GetUserRoles(ApplicationUser user);
        public Task<IdentityResult> AddRole(AddOrEditRoleDTO roleDto);
        public Task<IdentityResult> DeleteRole(string roleId);
        public Task<IdentityResult> EditRole(AddOrEditRoleDTO roleDto);
        public Task<AddOrEditRoleDTO> GetRoleDtoById(string roleId);
    }
}
