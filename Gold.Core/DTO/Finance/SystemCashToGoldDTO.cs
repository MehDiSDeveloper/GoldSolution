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
    public class SystemCashToGoldDTO
    { 
        [Remote("SysGoldUnitPriceValidator", "ManageGold")]
        public decimal GoldUnitPrice { get; set; }
        [Remote("SysTotalCashValidator", "ManageGold")]
        public decimal TotalCash { get; set; }
    }
}
