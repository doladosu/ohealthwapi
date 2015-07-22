using System.Data.Entity;
using Health.Data.Models;

namespace Health.Data.Context
{
    public partial class HealthDbContext : DbContext
    {
        public HealthDbContext()
            : base("name=HealthDbContext")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<ApplicationType> ApplicationTypes { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<State> States { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationType>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.ApplicationType1)
                .HasForeignKey(e => e.ApplicationType);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .Property(e => e.Dob)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Patients)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Race>()
                .HasMany(e => e.Patients)
                .WithRequired(e => e.Race)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);
        }
    }
}
