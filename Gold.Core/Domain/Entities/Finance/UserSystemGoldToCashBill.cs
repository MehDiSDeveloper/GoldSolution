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
    public class UserSystemGoldToCashBill
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column(TypeName = "decimal(18,2)")]

        public decimal CashAmount { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,4})?$", ErrorMessage = "حد اکثر 4 رقم اعشار قابل قبول است")]
        public double GoldAmount { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        

        //navigation propertis
        public UserGoldToCash? ToUserGoldToCash { get; set; }
        public SystemCashToGold? ToSystemCashToGold { get; set; }
    }
}
