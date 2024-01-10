using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CodeHub.Models
{
    public partial class Connection : DbContext
    {
        public Connection()
            : base("name=Connection")
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<DepositHistory> DepositHistories { get; set; }
        public virtual DbSet<DetailCode> DetailCodes { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<ImageUrl> ImageUrls { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportType> ReportTypes { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<SourceCode> SourceCodes { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ImageUrl>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Manager>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<Manager>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<Manager>()
                .HasMany(e => e.Requests)
                .WithOptional(e => e.Manager)
                .HasForeignKey(e => e.Coder);

            modelBuilder.Entity<Manager>()
                .HasMany(e => e.SourceCodes)
                .WithRequired(e => e.Manager)
                .HasForeignKey(e => e.Coder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<SourceCode>()
                .Property(e => e.LinkVideo)
                .IsUnicode(false);

            modelBuilder.Entity<SourceCode>()
                .Property(e => e.SourceLink)
                .IsUnicode(false);

            modelBuilder.Entity<SourceCode>()
                .HasMany(e => e.Carts)
                .WithOptional(e => e.SourceCode)
                .HasForeignKey(e => e.CodeID);

            modelBuilder.Entity<SourceCode>()
                .HasMany(e => e.DetailCodes)
                .WithRequired(e => e.SourceCode)
                .HasForeignKey(e => e.Source)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SourceCode>()
                .HasMany(e => e.ImageUrls)
                .WithRequired(e => e.SourceCode)
                .HasForeignKey(e => e.Source)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SourceCode>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.SourceCode)
                .HasForeignKey(e => e.CodeID);

            modelBuilder.Entity<SourceCode>()
                .HasMany(e => e.Reports)
                .WithOptional(e => e.SourceCode)
                .HasForeignKey(e => e.Source);

            modelBuilder.Entity<SourceCode>()
                .HasMany(e => e.Reviews)
                .WithOptional(e => e.SourceCode)
                .HasForeignKey(e => e.CodeID);

            modelBuilder.Entity<User>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reports)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Reporter);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Requests)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.Requester);
        }
    }
}
