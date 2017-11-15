using System.Data.Entity;
using EventService.Models;

namespace EventService.Database
{
    public class EventContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}