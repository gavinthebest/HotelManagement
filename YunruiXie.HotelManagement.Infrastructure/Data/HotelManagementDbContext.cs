using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YunruiXie.HotelManagement.Core.Entities;

namespace YunruiXie.HotelManagement.Infrastructure.Data
{
    public class HotelManagementDbContext : DbContext
    {
        public HotelManagementDbContext(DbContextOptions<HotelManagementDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ROOM>(ConfigureRoom);
            modelBuilder.Entity<CUSTOMER>(ConfigureCustomer);
            modelBuilder.Entity<ROOMTYPE>(ConfigureRoomtype);
            modelBuilder.Entity<SERVICE>(ConfigureMovieService);
        }
        private void ConfigureCustomer(EntityTypeBuilder<CUSTOMER> modelBuilder)
        {
            modelBuilder.HasOne(t => t.Room)
                        .WithOne(t => t.Customer);
        }
        private void ConfigureMovieService(EntityTypeBuilder<SERVICE> modelBuilder)
        {
            modelBuilder.HasOne(t => t.Room)
                        .WithMany(t => t.Services);
        }
        private void ConfigureRoom(EntityTypeBuilder<ROOM> modelBuilder)
        {
            modelBuilder.HasOne(t => t.Customer)
                        .WithOne(t => t.Room)
                        .HasForeignKey<CUSTOMER>(t => t.ROOMNO);
            modelBuilder.HasOne(t => t.Roomtype)
                        .WithMany(t => t.Rooms);
            modelBuilder.HasMany(t => t.Services)
                        .WithOne(t => t.Room);
        }
        private void ConfigureRoomtype(EntityTypeBuilder<ROOMTYPE> modelBuilder)
        {
            modelBuilder.HasMany(t => t.Rooms)
                        .WithOne(t => t.Roomtype);
        }
        public DbSet<ROOM> Rooms { get; set; }
        public DbSet<CUSTOMER> Customers { get; set; }
        public DbSet<ROOMTYPE> Roomtypes { get; set; }
        public DbSet<SERVICE> Services { get; set; }
    }
}
