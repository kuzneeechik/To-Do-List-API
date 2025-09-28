using Microsoft.EntityFrameworkCore;
using ToDoList_API.Data.Entities;

namespace ToDoList_API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<TaskEntity> Tasks { get; set; }

        public DataContext(DbContextOptions options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskEntity>().ToTable("Tasks");

            base.OnModelCreating(modelBuilder);
        }
    }
}
