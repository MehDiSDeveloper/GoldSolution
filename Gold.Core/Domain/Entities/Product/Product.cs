using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.Domain.Entities.Product
{
    public class Product
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "طلا";
        public string UnitName { get; set; } = "گرم";

        //navigation properties
        public ICollection<GoldPrice>? ToGoldPrices { get; set; }
    }
}
