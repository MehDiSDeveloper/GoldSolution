
using Gold.Core.Domain.Entities.Ticket;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Gold.Core.DTO.TicketDTO
{
    public class TicketMessageDTO
    {
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }
        public string? TicketId { get; set; }
        public string SetStatus { get; set; } = "";
    }
}
