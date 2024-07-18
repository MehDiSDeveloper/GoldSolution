using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.Domain.Entities.Ticket
{
    public class TicketMessage
    {
        [Key]
        public string MessageId { get; set; } = Guid.NewGuid().ToString();
        public string MessageSubject { get; set; } = "";
        public string MessageBody { get; set; } = "";
        public DateTime MessageTime { get; set; } = DateTime.Now;

        //nav props
        public Ticket Ticket { get; set; }
    }
}
