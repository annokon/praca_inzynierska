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
        
        modelBuilder.Entity<BlockedUser>(entity =>
        {
            // primary ket for id_user_blocker and id_user_blocked
            entity.HasKey(b => new { b.IdUserBlocker, b.IdUserBlocked });

            // foreign key for blocker -> user
            entity.HasOne(b => b.Blocker)
                .WithMany(u => u.BlockedUsers)
                .HasForeignKey(b => b.IdUserBlocker)
                .OnDelete(DeleteBehavior.Cascade);

            // foreign key for blocked -> user
            entity.HasOne(b => b.Blocked)
                .WithMany(u => u.UsersBlockingThisUser)
                .HasForeignKey(b => b.IdUserBlocked)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
    }
    
}