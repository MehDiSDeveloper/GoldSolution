using Gold.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.DTO.PayDTO
{
    public class PayBillDTO
    {
        public string Id { get; set; } = "";
                
        [Remote("SysTotalCashValidator", "ManageGold")]
        public decimal TotalCashAmount { get; set; }

        [Remote("SysTotalCashValidator", "ManageGold")]
        public decimal UserCashAsset { get; set; } = 0;

        public decimal CashToPay { get; set; } = 0;

        public DateTime? ExecutionTime { get; set; }
        public DateTime? BillRegDateTime { get; set; }       

        public bool IsPayed { get; set; }= false;
        public string DealType { get; set; } = ""; //deposit / withdraw / withdrawWallet
        public string DealName { get; set; } = ""; // خرید طلا/افزایش موجودی
        public string PayType { get; set; } = ""; // bank /bankwallet /wallet
        public decimal FinalPayment { get; set; }
        public string AssetId { get; set; } = "";


        //Navigation props
        public ApplicationUser? ToAppUser { get; set; }
    }
}
