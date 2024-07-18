using Gold.Core.Domain.Entities.Assets;
using Gold.Core.Domain.Entities.Finance;
using Gold.Core.Domain.IdentityEntities;
using Gold.Core.DTO.AccountDTO;
using Gold.Core.DTO.Finance;
using Gold.Core.DTO.PayDTO;
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
    public interface IPayServices
    {
        public Task setTransaction(PayBillDTO payBillDTO, string transId);
        public Task<Transaction?> getTransactionById(string transactionId);
        public Task setTransactionIsPayedById(string transactionId);
        public Task setUserCashGoldIsPayedById(string billId);
        public Task setUserAssetCashToZeroById(string assetId);
        public Task withdrawFromAssetByIdandAmount(string assetId, decimal moneyAmount);
        public Task<UserCashToGold> getBuyBillbyId(string Id);
        public Task<UserGoldAsset> getUserAssetByUser(ApplicationUser user);
    }
}
