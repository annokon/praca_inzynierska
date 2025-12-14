using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<Models.User> Users => Set<Models.User>();
    public DbSet<Models.Language> Languages => Set<Models.Language>();
    public DbSet<UserLanguage> UserLanguages => Set<UserLanguage>();


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
            entity.HasKey(b => new { b.IdUserBlocker, b.IdUserBlocked });

            entity.HasOne(b => b.Blocker)
                .WithMany(u => u.BlockedUsers)
                .HasForeignKey(b => b.IdUserBlocker)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(b => b.Blocked)
                .WithMany(u => u.UsersBlockingThisUser)
                .HasForeignKey(b => b.IdUserBlocked)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserLanguage>(entity =>
        {
            entity.HasKey(ul => new { ul.IdLanguage, ul.IdUser });

            entity.HasOne(ul => ul.Language)
                .WithMany(l => l.UserLanguages)
                .HasForeignKey(ul => ul.IdLanguage)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ul => ul.User)
                .WithMany(u => u.UserLanguages)
                .HasForeignKey(ul => ul.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserBadge>(entity =>
        {
            entity.HasKey(ub => new { ub.IdUser, ub.IdBadge });

            entity.HasOne(ub => ub.User)
                .WithMany(u => u.UserBadges)
                .HasForeignKey(ub => ub.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ub => ub.Badge)
                .WithMany(b => b.UserBadges)
                .HasForeignKey(ub => ub.IdBadge)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserChat>(entity =>
        {
            entity.HasKey(uc => new { uc.IdChat, uc.IdUser });

            entity.HasOne(uc => uc.Chat)
                .WithMany(c => c.UserChats)
                .HasForeignKey(uc => uc.IdChat)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(uc => uc.User)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uc => uc.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<UserInterest>(entity =>
        {
            entity.HasKey(ui => new { ui.IdInterest, ui.IdUser });

            entity.HasOne(ui => ui.Interest)
                .WithMany(i => i.UserInterests)
                .HasForeignKey(ui => ui.IdInterest)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ui => ui.User)
                .WithMany(u => u.UserInterests)
                .HasForeignKey(ui => ui.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserTransportMode>(entity =>
        {
            entity.HasKey(utm => new { utm.IdTransportMode, utm.IdUser });

            entity.HasOne(utm => utm.TransportMode)
                .WithMany(tm => tm.UserTransportModes)
                .HasForeignKey(utm => utm.IdTransportMode)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(utm => utm.User)
                .WithMany(u => u.UserTransportModes)
                .HasForeignKey(utm => utm.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
        });

    }
}