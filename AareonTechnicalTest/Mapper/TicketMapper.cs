using AareonTechnicalTest.Models;

namespace AareonTechnicalTest.Mapper
{
    public class TicketMapper
    {
        public static TicketUpdate GetUpdateTicket(Ticket Ticket)
        {
            var TicketUpdate =  new TicketUpdate();
            TicketUpdate.Id = Ticket.Id;
            TicketUpdate.PersonId = Ticket.PersonId;
            TicketUpdate.Content = Ticket.Content;

            return TicketUpdate;
        }
    }
}
