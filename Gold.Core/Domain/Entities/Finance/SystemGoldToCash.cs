using Gold.Core.Domain.Entities.Assets;
using System.ComponentModel.DataAnnotations;
using Gold.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Numerics;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gold.Core.Domain.Entities.Finance
{
    /// <summary>
    /// system offers amount of gold and unit price to sell gold to users
    /// </summary>

    public class SystemGoldToCash
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// the total amount of gold in gerams to be sold by system to users
        /// </summary>
        [RegularExpression(@"^\d+(\.\d{1,4})?$",ErrorMessage ="حد اکثر 4 رقم اعشار قابل قبول است")]
        public double TotalGold { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        /// <summary>
        /// the total price that system payed to buy gold 
        /// </summary>
        public decimal TotalCash { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        /// <summary>
        /// the price that is determined to sell one geram of gold to users
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// the amount of gold that has been sold in one bill until now
        /// </summary>
        [RegularExpression(@"^\d+(\.\d{1,4})?$",ErrorMessage ="حد اکثر 4 رقم اعشار قابل قبول است")]
        public double SoldGold { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,4})?$", ErrorMessage = "حد اکثر 4 رقم اعشار قابل قبول است")]
        public double LeftGold { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        /// <summary>
        /// the total cash system earned in this bill from selling gold to users until now
        /// </summary>
        public decimal EarnedCash { get; set; } 
        [Column(TypeName = "decimal(18,2)")]
        public decimal LeftCash { get; set; }
        public DateTime DateTime { get; set; }      
        public DateTime ExecutionTime { get; set; }      
        public long Delay { get; set; }
        /// <summary>
        /// is this bill deactivated by admin?
        /// </summary>
        public bool IsActive { get; set; } = false;
        /// <summary>
        /// is the whole bill sold?
        /// </summary>
        public bool IsDone { get; set; } = false;
        public SystemGoldToCash()
        {
            LeftCash = (decimal)TotalGold * UnitPrice;
            ToUserSystemCashToGoldBills = new HashSet<UserSystemCashToGoldBill>();
        }

        //navigation properties
        public ApplicationUser ToAppUser { get; set; }

        public ICollection<UserSystemCashToGoldBill>? ToUserSystemCashToGoldBills { get; set; }

    }
}
