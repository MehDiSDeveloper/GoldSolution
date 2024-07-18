using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.Domain.Entities.Product
{
    public class GoldPrice
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        //navigation props
    }
}
