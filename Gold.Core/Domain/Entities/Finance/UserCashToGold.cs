using Gold.Core.Domain.Entities.Assets;
using Gold.Core.Domain.IdentityEntities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.Domain.Entities.Finance;

public class UserCashToGold
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCash { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal LeftCash { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal SpentCash { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; } = 0;
    [RegularExpression(@"^\d+(\.\d{1,4})?$", ErrorMessage = "حد اکثر 4 رقم اعشار قابل قبول است")]
    public double EarnedGold { get; set; }
    public DateTime DateTime { get; set; }= DateTime.Now;
    public DateTime ExecutionTime { get; set; }
    public long Delay { get; set; }
    public bool IsPayed { get; set; } = false;
    public bool IsActive { get; set; } = false;
    public bool IsDone { get; set; } = false;
    public UserCashToGold()
    {
        ToUserSystemCashToGoldBills = new HashSet<UserSystemCashToGoldBill>();
    }

    //navigation properties
    //public UserGoldAsset? ToGoldAsset { get; set; }
    public ApplicationUser ToAppUser { get; set; }
    public ICollection<UserSystemCashToGoldBill>? ToUserSystemCashToGoldBills { get; set; }
}
