using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.DTO.AccountDTO
{
    public class SearchUserBillDTO
    {
        public string? SearchField { get; set; }

        public string? SearchWord { get; set; }

        public string? Status { get; set; }
       
        public string? Type { get; set; }
    }
}
