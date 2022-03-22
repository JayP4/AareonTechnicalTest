using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Models
{
    public static class NoteConfig
    {
        public static void Configure(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Note>().HasKey(e => e.Id);
           
        }
    }
}
