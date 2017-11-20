using System.Data.Entity;
using EventService.Models;
using Microsoft.EntityFrameworkCore;

namespace EventService.Database
{
    public class EventContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public System.Data.Entity.DbSet<Event> Events { get; set; }
        public System.Data.Entity.DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=eventsdb.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure domain classes using Fluent API here
            base.OnModelCreating(modelBuilder);
        }
    }
}