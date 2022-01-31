using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace YotorContext.Models
{
    public partial class YotorDbContext : DbContext
    {
        public YotorDbContext()
        {
        }

        public YotorDbContext(DbContextOptions<YotorDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Backup> Backups { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Landlord> Landlords { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Restriction> Restrictions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=YotorDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Backup>(entity =>
            {
                entity.ToTable("Backup");

                entity.Property(e => e.BackupId).HasColumnName("backup_id");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.EndAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("end_address");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.FullPrice).HasColumnName("full_price");

                entity.Property(e => e.RestrictionId).HasColumnName("restriction_id");

                entity.Property(e => e.StartAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("start_address");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_Car");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.FeedbackId)
                    .HasConstraintName("FK_Booking_Feedback");

                entity.HasOne(d => d.Restriction)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.RestrictionId)
                    .HasConstraintName("FK_Booking_Restriction");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_Customer");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.Property(e => e.CarId).HasColumnName("car_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("brand");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("model");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("number");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.Photo).HasColumnName("photo");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Transmission)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("transmission");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("type");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("year");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car_Organization");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Customer");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.DriversLicense).HasColumnName("drivers_license");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("full_name");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.Passport).HasColumnName("passport");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.Photo).HasColumnName("photo");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("text");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Customer");
            });

            modelBuilder.Entity<Landlord>(entity =>
            {
                entity.ToTable("Landlord");

                entity.Property(e => e.LandlordId).HasColumnName("landlord_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Landlords)
                    .HasForeignKey(d => d.OrganizationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Landlord_Organization");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Landlords)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Landlord_Customer");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization");

                entity.Property(e => e.OrganizationId).HasColumnName("organization_id");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("account");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("code");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Founder)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("founder");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.Taxes)
                    .IsRequired()
                    .HasColumnName("taxes");
            });

            modelBuilder.Entity<Restriction>(entity =>
            {
                entity.ToTable("Restriction");

                entity.Property(e => e.RestrictionId).HasColumnName("restriction_id");

                entity.Property(e => e.CarName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("car_name");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.LandlordId).HasColumnName("landlord_id");

                entity.HasOne(d => d.Landlord)
                    .WithMany(p => p.Restrictions)
                    .HasForeignKey(d => d.LandlordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Restriction_Landlord");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
