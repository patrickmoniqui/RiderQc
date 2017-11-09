namespace RiderQc.Web.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RiderQcContext : DbContext
    {
        public RiderQcContext()
            : base("name=RiderQcContext")
        {
        }

        public virtual DbSet<Authentification> Authentifications { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Moto> Motoes { get; set; }
        public virtual DbSet<Ride> Rides { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Trajet> Trajets { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLevel> UserLevels { get; set; }
        public virtual DbSet<UserRating> UserRatings { get; set; }
        public virtual DbSet<UserRide> UserRides { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authentification>()
                .Property(e => e.Token)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.ChildComments)
                .WithOptional(e => e.ParentComment)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.Rides)
                .WithRequired(e => e.Level)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Level>()
                .HasMany(e => e.UserLevels)
                .WithRequired(e => e.Level)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ride>()
                .HasMany(e => e.UserRides)
                .WithRequired(e => e.Ride)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Authentifications)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.SenderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ReceiverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Messages1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.SenderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Motoes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CreatedRides)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CreatorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Trajets)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CreatorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRatings)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.RatedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRatings1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.RaterId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRides)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserLevels)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
