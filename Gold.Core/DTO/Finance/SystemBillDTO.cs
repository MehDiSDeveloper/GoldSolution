using Gold.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.DTO.Finance
{
    public class SystemBillDTO
    {
        public string Id { get; set; } = "";
        /// <summary>
        /// the total amount of gold in gerams to be sold by system to users
        /// </summary>
        /// 
        [Remote("SysCashAmountValidator", "ManageGold")]
        public double TotalGoldAmount { get; set; }
        
        [Remote("SysCashAmountValidator", "ManageGold")]
        public decimal TotalCashAmount { get; set; }

        [Remote("SysCashAmountValidator", "ManageGold")]
        public decimal SpentCash { get; set; } = 0;

        [Remote("SysCashAmountValidator", "ManageGold")]
        public decimal LeftCash { get; set; } = 0;

        [Remote("SysCashAmountValidator", "ManageGold")]
        public decimal EarnedCash { get; set; } = 0;

        [Remote("SysCashAmountValidator", "ManageGold")]
        public double SpentGold { get; set; } = 0;

        [Remote("SysCashAmountValidator", "ManageGold")]
        public double EarnedGold { get; set; } = 0;

        [Remote("SysCashAmountValidator", "ManageGold")]
        public double LeftGold { get; set; } = 0;

        [Remote("SysCashAmountValidator", "ManageGold")]
        public decimal UnitPrice { get; set; } = 0;
        public double DonePercent { get; set; }
        public string Type { get; set; } = "";
        public DateOnly Date { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime ExecutionTime { get; set; }
        public long Delay { get; set; }
        public bool IsDone { get; set; }
        public bool IsActive { get; set; }
       
        //Navigation props
        public ApplicationUser? ToAppUser { get; set; }
    }
}
