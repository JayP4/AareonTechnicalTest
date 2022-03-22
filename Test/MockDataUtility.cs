using AareonTechnicalTest;
using AareonTechnicalTest.Models;
using System.Collections.Generic;

namespace Test
{
    internal class MockDataUtility
    {
        public static List<Note> NotesMock()
        {
            return new List<Note>
            {
                new() {
                PersonId = 1,
                TicketId = 1,
                Content = "Check power supply." },
                new() {
                PersonId = 1,
                TicketId = 1,
                Content = "Check temprature." },
            };
        }
        public static void CreatePersonAndTicket(ApplicationContext  context)
        {
            var person = new Person() {
                Forename = "Michel",
                Surname = "Muller",
                IsAdmin = true
            };
            var ticket = new Ticket()
            {
                PersonId = 1,
                Content = "Priority normal"
            };
            context.Persons.Add(person);
            context.Tickets.Add(ticket);
            context.SaveChanges();
        }
        public static void CreateNotes(ApplicationContext context)
        {
            var noteLists = new Note[] {
                new() {
                PersonId = 1,
                TicketId = 1,
                Content = "Check power supply." },
                new() {
                PersonId = 1,
                TicketId = 1,
                Content = "Check temprature." },

                };
            context.Notes.AddRange(noteLists);
            context.SaveChanges();
        }

    }
}
