using Gold.Core.Domain.IdentityEntities;
using Gold.Core.DTO.AccountDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.ServiceContracts
{
    public interface IAccountServices
    {
        public Task<ApplicationUser?> GetUserByUserName(string userName);
        public Task<ApplicationUser> GetUserByPhone(string userPhone);
        public Task<string> GetUserIdByName(string? userName);
        public Task<ApplicationUser> GetUserById(string id);
        public Task<IdentityResult> AddUser(RegisterUserDTO regUserDto);
        public Task<bool> EditUser(EditUserDTO edtUserDto,List<string> selectedRoles);        
        public Task<IdentityResult> DeleteUserByUserName(string userName);
        public Task<List<ShowUserDTO>> GetAllUsers();
        public Task<ApplicationUser> GetLogedinUser(HttpContext httpContext);
        public Task<IdentityResult> SetRolesToUser(ApplicationUser user, List<string> selectedRoles);

    }
}
