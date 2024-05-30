using Microsoft.EntityFrameworkCore;
using Api1.DatabaseModels;

namespace Api1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<StudentDog> StudentDogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }

}
