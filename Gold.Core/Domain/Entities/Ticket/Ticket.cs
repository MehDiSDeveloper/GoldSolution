using Gold.Core.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.Domain.Entities.Ticket
{
    public class Ticket
    {
        [Key]
        public string TicketId { get; set; } = Guid.NewGuid().ToString();
        public string TicketTitle { get; set; } = "";
        public Status TicketStatus { get; set; } = Status.Pending;
        public DateTime TicketDateTime { get; set; }= DateTime.Now;
        public int TicketDepartment { get; set; } = 0;


        //nav props
        public ICollection<TicketMessage> TicketToMessages { get; set; }
        public ApplicationUser TicketToUser { get; set; }

        public enum Status 
        {
            Pending,
            Replyed,
            Closed
        }

    }
}
