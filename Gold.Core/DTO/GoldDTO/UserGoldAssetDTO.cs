using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.DTO.GoldDTO
{
    public class UserGoldAssetDTO
    {
        public string Id { get; set; }
        public double GoldAmount { get; set; }
        /// <summary>
        /// total money user payed to system to get gold until now
        /// </summary>
        public decimal PayedTotalCash { get; set; }
        /// <summary>
        /// total money user taken from selling gold to system
        /// </summary>
        public decimal EarnedTotalCash { get; set; }
        /// <summary>
        /// total value of gold asset stored in user account
        /// </summary>
        public decimal TotalCashAsset { get; set; }
    }
}
