using Gold.Core.Domain.Entities.Assets;
using Gold.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gold.Core.Domain.Entities.Finance;

public class UserGoldToCash
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [RegularExpression(@"^\d+(\.\d{1,4})?$",ErrorMessage ="حد اکثر 4 رقم اعشار قابل قبول است")]
    public double TotalGold { get; set; }
    [RegularExpression(@"^\d+(\.\d{1,4})?$",ErrorMessage ="حد اکثر 4 رقم اعشار قابل قبول است")]
    public double SpentGold { get; set; }
    public double LeftGold { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal EarnedCash { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; } = 0;
    public DateTime DateTime { get; set; }
    public DateTime ExecutionTime { get; set; }
    public long Delay { get; set; }
    public bool IsPayed { get; set; } = false;
    public bool IsActive { get; set; } = false;
    public bool IsDone { get; set; } = false;
    public UserGoldToCash()
    {
        ToUserSystemGoldToCashBills = new HashSet<UserSystemGoldToCashBill>();
    }

    //navigation properties
    //public UserGoldAsset? ToGoldAsset { get; set; }
    public ApplicationUser ToAppUser { get; set; }
    public ICollection<UserSystemGoldToCashBill>? ToUserSystemGoldToCashBills { get; set; }

}
