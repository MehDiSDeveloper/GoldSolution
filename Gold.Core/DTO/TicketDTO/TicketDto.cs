
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static Gold.Core.Domain.Entities.Ticket.Ticket;

namespace Gold.Core.DTO.TicketDTO
{
    public class TicketDTO
    {
        public string? TicketId { get; set; }
        public string TicketTitle { get; set; }
        public int TicketDepartment { get; set; }
        public Status TicketStatus { get; set; }
        public DateTime TicketDateTime { get; set; }

    }
}
