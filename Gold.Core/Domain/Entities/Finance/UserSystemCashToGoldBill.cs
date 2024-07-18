using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.Domain.Entities.Finance
{
    public class UserSystemCashToGoldBill
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column(TypeName = "decimal(18,2)")]
        public decimal CashAmount { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,4})?$", ErrorMessage = "حد اکثر 4 رقم اعشار قابل قبول است")]
        public double GoldAmount { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public int MyProperty { get; set; }

        //navigation propertis
        public UserCashToGold? ToUserCashToGold { get; set; }
        public SystemGoldToCash? ToSystemGoldToCash { get; set; }
    }
}
