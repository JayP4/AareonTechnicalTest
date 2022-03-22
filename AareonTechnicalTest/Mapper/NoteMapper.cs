using AareonTechnicalTest.Models;

namespace AareonTechnicalTest.Mapper
{
    public class NoteMapper
    {
        public static NoteUpdate GetUpdateNote(Note note)
        {
            var noteUpdate =  new NoteUpdate();
            noteUpdate.Id = note.Id;
            noteUpdate.TicketId = note.TicketId;
            noteUpdate.PersonId = note.PersonId;
            noteUpdate.Content = note.Content;

            return noteUpdate;
        }
    }
}
