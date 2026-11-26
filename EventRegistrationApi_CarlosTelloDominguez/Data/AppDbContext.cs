using EventRegistrationApi_CarlosTelloDominguez.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationApi_CarlosTelloDominguez.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Registrations)
                .WithOne(r => r.Event)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    EventId = 1,
                    Title = "Intro to Programming",
                    Location = "Sheridan College",
                    EventDate = new DateTime(2026, 11, 15),
                    Capacity = 200
                },
                new Event
                {
                    EventId = 2,
                    Title = "Swimming WorkShop",
                    Location = "Sheridan College",
                    EventDate = new DateTime(2026, 6, 7),
                    Capacity = 50
                },

                new Event
                {
                    EventId = 3,
                    Title = "Calculus WorkShop",
                    Location = "UOFT College",
                    EventDate = new DateTime(2026, 9, 20),
                    Capacity = 100
                },

                new Event
                {
                    EventId = 4,
                    Title = "Security Workshop",
                    Location = "Waterloo University",
                    EventDate = new DateTime(2026, 10, 5),
                    Capacity = 150
                }
            );

            modelBuilder.Entity<Registration>().HasData(
                new Registration
                {
                    RegistrationId = 1,
                    EventId = 1,
                    StudentName = "Carlos Tello Dominguez",
                    Email = "carlos@email.com",
                    RegisteredOn = new DateTime(2026, 5, 1)
                },

                new Registration
                {
                    RegistrationId = 2,
                    EventId = 2,
                    StudentName = "John Doe",
                    Email = "john@email.com",
                    RegisteredOn = new DateTime(2026, 5, 2)
                },
                new Registration
                {
                    RegistrationId = 3,
                    EventId = 3,
                    StudentName = "Aaron Smith",
                    Email = "aaron@email.com",
                    RegisteredOn = new DateTime(2026, 5, 3)
                },

                new Registration
                {
                    RegistrationId = 4,
                    EventId = 4,
                    StudentName = "Alice Tank",
                    Email = "alice@email.com",
                    RegisteredOn = new DateTime(2026, 5, 4)
                },
                new Registration
                {
                    RegistrationId = 5,
                    EventId = 1,
                    StudentName = "Bob Brown",
                    Email = "bob@email.com",
                    RegisteredOn = new DateTime(2026, 5, 5)
                }
            );

        }
    }

}