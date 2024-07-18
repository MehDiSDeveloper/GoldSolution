using Gold.Core.Domain.Entities.Assets;
using Gold.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.DTO.PayDTO;

public class TransactionDTO
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? PayBillId { get; set; }
    public string PayWay { get; set; } = ""; // bank /bankwallet /wallet
    public string DealName { get; set; } = ""; // خرید طلا/افزایش موجودی
    public string DealType { get; set; } = ""; // واریز/ برداشت
    public decimal Cash { get; set; } = 0;
    public decimal AssetCash { get; set; } = 0;
    public DateTime DateTime { get; set; } = DateTime.Now;
    public DateTime? ExecutionTime { get; set; }
    public bool IsPayed { get; set; } = false;

    //navigation properties
    //public UserGoldAsset? ToGoldAsset { get; set; }
    public ApplicationUser ToAppUser { get; set; }
}
