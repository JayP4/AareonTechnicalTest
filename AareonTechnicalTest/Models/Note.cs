using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Models
{
    public class Note
    {
        [Key]
        public int Id { get; }
        public int TicketId { get; set; }
        public int PersonId { get; set; }
        public string Content { get; set; }
    }
    public class NoteUpdate : Note
    {
        [Key]
        public int Id { get; set; }
    }
}
