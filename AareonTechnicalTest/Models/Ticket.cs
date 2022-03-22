using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; }
        public string Content { get; set; }
        public int PersonId { get; set; }
        public ICollection<Note> Notes { get; set; }

    }
    public class TicketUpdate : Ticket
    {
        [Key]
        public int Id { get; set; }
    }
}
