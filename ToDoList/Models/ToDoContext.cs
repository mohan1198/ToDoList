using Microsoft.EntityFrameworkCore;
namespace ToDoList.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext()
        {
        }

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
            Database.Migrate();
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          var connection = "Server=(LocalDb)\\MSSQLLocalDB;Database=ToDoDB;Integrated Security=True;MultipleActiveResultSets=true;";
          optionsBuilder.UseSqlServer(connection);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>().HasIndex(a => a.Email)
            .IsUnique();

        }


    }
}