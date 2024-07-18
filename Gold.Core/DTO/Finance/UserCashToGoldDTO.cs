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
    public class UserCashToGoldDTO
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        //[Range(5000,1000000000)]
        [Remote("CashAmountValidator","Gold")]
        public decimal CashAmount { get; set; }
        public decimal? CashExecutedAmount { get; set; } = 0;
        public decimal? GoldUnitPrice { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public bool IsPayed { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public bool IsDone { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
