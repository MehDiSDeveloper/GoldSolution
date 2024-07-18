using Gold.Core.Domain.Entities.Assets;
using Gold.Core.Domain.IdentityEntities;
using Gold.Core.DTO.AccountDTO;
using Gold.Core.ServiceContracts;
using Gold.Infrastructure.GoldDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gold.Infrastructure.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _Context;
        public AccountServices(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext Context)
        {
            _userManager = userManager;
            _Context = Context;
        }

        public async Task<bool> EditUser(EditUserDTO edtUserDto, List<string> selectedRoles)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(edtUserDto.Id);
            if (user == null)
                return false;
            user.UserName = edtUserDto.UserName;
            user.FirstName = edtUserDto.FirstName;
            user.LastName = edtUserDto.LastName;
            user.PhoneNumber = edtUserDto.Phone;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return false;

            var Roles = await _userManager.GetRolesAsync(user);
            var result2 = await _userManager.RemoveFromRolesAsync(user, Roles);
            if (!result2.Succeeded)
                return false;

            var result3 = await _userManager.AddToRolesAsync(user, selectedRoles);
            if (!result3.Succeeded)
                return false;

            return true;
        }

        public async Task<List<ShowUserDTO>> GetAllUsers()
        {
            List<ShowUserDTO> showUsers = await _userManager.Users.Select(u => new ShowUserDTO
            {
                UserName = u.NormalizedUserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Phone = u.PhoneNumber,
                CreateDateTime = u.CreatDateTime,
                UserRoles = ""
            }).OrderByDescending(u => u.CreateDateTime).ToListAsync();
            foreach (var showUser in showUsers)
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.NormalizedUserName == showUser.UserName);
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (userRoles != null)
                    {
                        foreach (var role in userRoles)
                        {
                            showUser.UserRoles += role + " ";
                        }
                    }
                }
            }
            return showUsers;
        }

        public async Task<IdentityResult> AddUser(RegisterUserDTO regUserDto)
        {
            ApplicationUser user = new()
            {
                UserName = regUserDto.UserName,
                FirstName = regUserDto.FirstName,
                LastName = regUserDto.LastName,
                PhoneNumber = regUserDto.Phone
            };
            IdentityResult result = await _userManager.CreateAsync(user, regUserDto.Password);
            if (result.Succeeded)
            {
                UserGoldAsset asset = new()
                {
                    TotalCashAsset = 0,
                    GoldAmount = 0,
                    ToAppUser = user
                };
                await _Context.UserGoldAssets.AddAsync(asset);
                await _Context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<ApplicationUser?> GetUserByUserName(string? userName)
        {
            if(userName == null)
                return null;
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> GetLogedinUser(HttpContext httpContext)
        {
            return await _userManager.GetUserAsync(httpContext.User);
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser> GetUserByPhone(string userPhone)
        {
            return await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == userPhone);
        }

        public async Task<IdentityResult> DeleteUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<IdentityResult> SetRolesToUser(ApplicationUser user, List<string> selectedRoles)
        {
            var Roles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.RemoveFromRolesAsync(user, Roles);
            if (!result.Succeeded)
                return result;
            var result2 = await _userManager.AddToRolesAsync(user, selectedRoles);
            if (!result2.Succeeded)
                return result2;

            return IdentityResult.Success;
        }

        public async Task<string> GetUserIdByName(string? userName)
        {
            if (userName == null)
                return string.Empty;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return string.Empty;
            }
            return user.Id;
        }
    }
}
