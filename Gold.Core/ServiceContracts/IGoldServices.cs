using Gold.Core.Domain.Entities.Finance;
using Gold.Core.DTO.AccountDTO;
using Gold.Core.DTO.Finance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.ServiceContracts
{
    public interface IGoldServices
    {
        public Task<List<SystemBillDTO>> GetSysBills(string status, string type, string searchString);
        public Task<List<UserBillDTO>> GetUsersBills(string status, string type, string searchString); 
        public Task<List<UserBillDTO>> GetUserBills(HttpContext httpContext);
        public Task<double> GetUserAssets(HttpContext httpContext);
        public Task<List<UserCashToGoldDTO>> GetusersGoldDemands();
        public Task<List<UserGoldToCashDTO>> GetusersGoldOffers();
        
        public Task<bool> SetUserCashToGold(UserCashToGoldDTO uC2GDto, HttpContext httpContext);
        public Task<bool> SetUserGoldToCash(UserGoldToCashDTO uG2CDto, HttpContext httpContext);
        public Task<bool> SetSystemGoldToCash(SystemGoldToCashDTO sysG2CDto, HttpContext httpContext);
        public Task<bool> SetSystemCashToGold(SystemCashToGoldDTO sysC2GDto, HttpContext httpContext);
              
        public Task<string> SwitchSusspendUserBuyBill(string Id);
        public Task<string> SwitchSusspendUserSellBill(string Id);
        public Task<string> SwitchSusspendSysBuyBill(string Id);
        public Task<string> SwitchSusspendSysSellBill(string Id);


        #region Logics
        public Task<string> ExecuteSysSellbill(string Id);
        public Task<string> ExecuteSysBuybill(string Id);
        
        public Task<string> ExecuteUserSellBill(string Id);
        public Task<string> ExecuteUserBuyBill(string Id);


        #endregion
    }
}
