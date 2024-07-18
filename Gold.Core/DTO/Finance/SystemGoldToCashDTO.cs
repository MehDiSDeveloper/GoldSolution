using Gold.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.DTO.Finance
{
    public class SystemGoldToCashDTO
    {
        /// <summary>
        /// the total amount of gold in gerams to be sold by system to users
        /// </summary>
        /// 
        [RegularExpression(@"^\d+(\.\d{1,4})?$",ErrorMessage ="حد اکثر 4 رقم اعشار قابل قبول است")]
        [Remote("SysGoldAmountValidator", "ManageGold")]
        public double GoldAmount { get; set; }
        /// <summary>
        /// the total price that system payed to buy gold 
        /// </summary>
        [Remote("SysTotalBoughtPriceValidator", "ManageGold")]
        public decimal TotalBoughtPrice { get; set; }
    }
}
