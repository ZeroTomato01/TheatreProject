using Microsoft.EntityFrameworkCore;
using TheatreProject.Utils;

namespace TheatreProject.Models
{
    public class DatabaseContext : DbContext
    {
        // The admin table will be used in both cases
        public DbSet<Admin> Admin { get; set; }

        // You can comment out or remove the case you are not going to use.

        // Tables for the Theatre ticket case

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<TheatreShowDate> TheatreShowDate { get; set; }
        public DbSet<TheatreShow> TheatreShow { get; set; }
        public DbSet<Venue> Venue { get; set; }

        // Tables for the event calendar case

        // public DbSet<User> User { get; set; }
        // public DbSet<Attendance> Attendance { get; set; }
        // public DbSet<Event_Attendance> Event_Attendance { get; set; }
        // public DbSet<Event> Event { get; set; }



        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .HasIndex(p => p.UserName).IsUnique();

            modelBuilder.Entity<Admin>()
                .HasData(new Admin { AdminId = 1, Email = "admin1@example.com", UserName = "admin1", Password = EncryptionHelper.EncryptPassword("password") });
            modelBuilder.Entity<Admin>()
                .HasData(new Admin { AdminId = 2, Email = "admin2@example.com", UserName = "admin2", Password = EncryptionHelper.EncryptPassword("tooeasytooguess") });
            modelBuilder.Entity<Admin>()
                .HasData(new Admin { AdminId = 3, Email = "admin3@example.com", UserName = "admin3", Password = EncryptionHelper.EncryptPassword("helloworld") });
            modelBuilder.Entity<Admin>()
                .HasData(new Admin { AdminId = 4, Email = "admin4@example.com", UserName = "admin4", Password = EncryptionHelper.EncryptPassword("Welcome123") });
            modelBuilder.Entity<Admin>()
                .HasData(new Admin { AdminId = 5, Email = "admin5@example.com", UserName = "admin5", Password = EncryptionHelper.EncryptPassword("Whatisapassword?") });
            
            // Venue new_venue = new Venue { VenueId = 1, Name = "The Pen", Capacity = 100, TheatreShows = new List<TheatreShow>()};
            // modelBuilder.Entity<Venue>()
            //     .HasData(new_venue);
            // modelBuilder.Entity<TheatreShow>()
            //     .HasData(new TheatreShow { TheatreShowId = 1, Title = "Cow's Movie", Description = "moo", Price = 20, 
            //     TheatreShowDates = {},
            //     Venue = {}});

            modelBuilder.Entity<Reservation>()
                .HasData(
                new Reservation{
                    ReservationId = 1,
                    AmountOfTickets = 1,
                    Used = false,
                    TheatreShowDateId = 1

                }
            );



            modelBuilder.Entity<Venue>()
                .HasData(
                new Venue{
                    VenueId = 1, 
                    Name = "The Pen", 
                    Capacity = 300
                }
            );

             modelBuilder.Entity<Venue>()
                .HasData(
                new Venue{
                    VenueId = 2, 
                    Name = "The Pigsty", 
                    Capacity = 50
                }
            );

            modelBuilder.Entity<TheatreShow>()
                .HasData(
                new TheatreShow{
                    TheatreShowId = 1,
                    Title = "Cow's Movie",
                    Description = "moo",
                    Price = 20,
                    VenueId = 1,
                    TheatreShowDateIds = new List<int> {1} 
                }
            );
            modelBuilder.Entity<TheatreShow>()
                .HasData(
                new TheatreShow{
                    TheatreShowId = 2,
                    Title = "Cow's Movie 2",
                    Description = "moo too",
                    Price = 20,
                    VenueId = 1,
                    TheatreShowDateIds = new List<int> {2, 3} 
                }
            );

            modelBuilder.Entity<TheatreShowDate>()
                .HasData(
                new TheatreShowDate{
                    TheatreShowDateId = 1,
                    DateAndTime = DateTime.Now.AddDays(7),
                    TheatreShowId = 1 
                }
            );
            modelBuilder.Entity<TheatreShowDate>()
                .HasData(
                new TheatreShowDate{
                    TheatreShowDateId = 2,
                    DateAndTime = DateTime.Now.AddDays(-7),
                    TheatreShowId = 2 
                }
            );
            modelBuilder.Entity<TheatreShowDate>()
                .HasData(
                new TheatreShowDate{
                    TheatreShowDateId = 3,
                    DateAndTime = DateTime.Now.AddDays(3),
                    TheatreShowId = 2 
                }
            );
        }

    }

}