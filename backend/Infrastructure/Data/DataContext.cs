using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasOne(n => n.TripInvitation)
                .WithOne(ti => ti.Notification)
                .HasForeignKey<Notification>(n => n.IdTripInvitation);
            entity.HasOne(n => n.Message)
                .WithMany(m => m.Notifications) 
                .HasForeignKey(n => n.IdMessage)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

}