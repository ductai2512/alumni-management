using AlumniManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlumniManagement.DAL.Data
{
    public class AlumniDbContext : DbContext
    {
        public AlumniDbContext(DbContextOptions<AlumniDbContext> options) : base(options)
        {

        }

        public DbSet<Alumni> Alumni { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<AlumniEvent> AlumniEvents { get; set; }
        public DbSet<Donation> Donations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Alumni - Account (One-to-One)
            modelBuilder.Entity<Alumni>()
                .HasOne(a => a.Account)
                .WithOne(ac => ac.Alumni)
                .HasForeignKey<Account>(ac => ac.AlumniId)
                .OnDelete(DeleteBehavior.Cascade);

            // Alumni - Donation (One-to-Many)
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Alumni)
                .WithMany(a => a.Donations)
                .HasForeignKey(d => d.AlumniId)
                .OnDelete(DeleteBehavior.Cascade);

            // AlumniEvent (Many-to-Many)
            modelBuilder.Entity<AlumniEvent>()
                .HasKey(ae => new { ae.AlumniId, ae.EventId });

            modelBuilder.Entity<AlumniEvent>()
                .HasOne(ae => ae.Alumni)
                .WithMany(a => a.AlumniEvents)
                .HasForeignKey(ae => ae.AlumniId);

            modelBuilder.Entity<AlumniEvent>()
                .HasOne(ae => ae.Event)
                .WithMany(e => e.AlumniEvents)
                .HasForeignKey(ae => ae.EventId);

            // Indexes
            modelBuilder.Entity<Alumni>()
                .HasIndex(a => a.StudentCode)
                .IsUnique();

            modelBuilder.Entity<Alumni>()
                .HasIndex(a => a.Email)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Username)
                .IsUnique();

            // Default values
            modelBuilder.Entity<Alumni>()
                .Property(a => a.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Account>()
                .Property(a => a.IsLocked)
                .HasDefaultValue(false);

            modelBuilder.Entity<Event>()
                .Property(e => e.IsCanceled)
                .HasDefaultValue(false);

            // Decimal precision
            modelBuilder.Entity<Donation>()
                .Property(d => d.Amount)
                .HasPrecision(18, 2);
        }
    }
}
