using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Booking
{
    public partial class bookingContext : DbContext
    {
        public bookingContext()
        {
        }

        public bookingContext(DbContextOptions<bookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<booking> Bookings { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=poastgres;Username=postgres;Password=root");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251@icu");

            modelBuilder.Entity<booking>(entity =>
            {
               // entity.HasNoKey();

                entity.ToTable("booking");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Begindate)
                    .HasColumnType("date")
                    .HasColumnName("begindate");

                entity.Property(e => e.Enddate)
                    .HasColumnType("date")
                    .HasColumnName("enddate");

                entity.Property(e => e.Idofroom).HasColumnName("idofroom");
                entity.Property(e => e.Idofhotel).HasColumnName("h_id");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.IdofroomNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idofroom)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("booking_idofroom_fkey");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("booking_userid_fkey");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("hotel");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("city");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.Isfreerooms).HasColumnName("isfreerooms");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("room");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HId).HasColumnName("h_id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Seats).HasColumnName("seats");

                entity.HasOne(d => d.HIdNavigation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("room_h_id_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
