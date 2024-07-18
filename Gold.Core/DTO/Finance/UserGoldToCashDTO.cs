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
    public class UserGoldToCashDTO
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        //[Remote("GoldAmountValidator","Gold")] gold asset check
        [RegularExpression(@"^\d+(\.\d{1,4})?$", ErrorMessage = "حد اکثر 4 رقم اعشار قابل قبول است")]
        public double GoldAmount { get; set; }        
        [RegularExpression(@"^\d+(\.\d{1,4})?$", ErrorMessage = "حد اکثر 4 رقم اعشار قابل قبول است")]
        public double GoldExecutedAmount { get; set; } = 0;
        public decimal? GoldUnitPrice { get; set; }
        public DateTime DateTime { get; set; }=DateTime.Now;
        public bool IsPayed { get; set; } = false;
        public bool IsDone { get; set; }
        public bool IsActive { get; set; } = false;
        public ApplicationUser? User { get; set; }
    }
}
