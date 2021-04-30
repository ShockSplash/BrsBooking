using Microsoft.EntityFrameworkCore;
using Booking.Models;

namespace Booking.Data
{
    public partial class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Users> Users { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("hotel");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("city");

                entity.Property(e => e.IsFreeRooms).HasColumnName("isfreerooms");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("name");

                entity.Property(e => e.Descritption)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("room");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.H_id).HasColumnName("h_id");

                entity.Property(e => e.IsFree).HasColumnName("isfree");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Seats).HasColumnName("seats");

            });
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Login).HasColumnName("login");

                entity.Property(e => e.Password).HasColumnName("password");


            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}