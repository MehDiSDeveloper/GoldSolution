using Gold.Core.Domain.Entities.Assets;
using Gold.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.Domain.Entities.Finance
{
    /// <summary>
    /// system offers amount of money and unit price to buy gold from users
    /// </summary>
    public class SystemCashToGold
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column(TypeName = "decimal(18,2)")]
        /// <summary>
        /// total cash determined by system to buy gold from users
        /// </summary>
        public decimal TotalCash { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        /// <summary>
        /// the price of one geram system pays to buy gold from users
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// the amount of gold system bought from users till now
        /// </summary>
        [RegularExpression(@"^\d+(\.\d{1,4})?$", ErrorMessage = "حد اکثر 4 رقم اعشار قابل قبول است")]
        public double EarnedGold { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        /// <summary>
        /// total cash payed to users till now
        /// </summary>
        public decimal SpentCash { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LeftCash { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime ExecutionTime { get; set; }
        public long Delay { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsDone { get; set; } = false;
        public SystemCashToGold()
        {
            ToUserSystemGoldToCashBills = new HashSet<UserSystemGoldToCashBill>();
        }


        //navigation properties
        public ApplicationUser ToAppUser { get; set; }

        public ICollection<UserSystemGoldToCashBill>? ToUserSystemGoldToCashBills { get; set; }
    }
}
