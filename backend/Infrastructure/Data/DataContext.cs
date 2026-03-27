using backend.Models;
using backend.Options;
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
    public DbSet<Models.Interest> Interests => Set<Models.Interest>();
    public DbSet<GenderOption> GenderOptions => Set<GenderOption>();
    public DbSet<PronounOption> PronounOptions => Set<PronounOption>();
    public DbSet<PersonalityTypeOption> PersonalityTypeOptions => Set<PersonalityTypeOption>();
    public DbSet<AlcoholPreferenceOption> AlcoholPreferenceOptions => Set<AlcoholPreferenceOption>();
    public DbSet<SmokingPreferenceOption> SmokingPreferenceOptions => Set<SmokingPreferenceOption>();
    public DbSet<DrivingLicenseOption> DrivingLicenseOptions => Set<DrivingLicenseOption>();
    public DbSet<TravelExperienceOption> TravelExperienceOptions => Set<TravelExperienceOption>();

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
        
        modelBuilder.Entity<UserTravelStyle>(entity =>
        {
            entity.HasKey(uts => new { uts.IdTravelStyle, uts.IdUser });

            entity.HasOne(uts => uts.TravelStyle)
                .WithMany(tm => tm.UserTravelStyles)
                .HasForeignKey(uts => uts.IdTravelStyle)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(uts => uts.User)
                .WithMany(u => u.UserTravelStyles)
                .HasForeignKey(uts => uts.IdUser)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<GenderOption>().HasData(
            new GenderOption { Id = 1, NameEn = "Male", NamePl = "Mężczyzna" },
            new GenderOption { Id = 2, NameEn = "Female", NamePl = "Kobieta" },
            new GenderOption { Id = 3, NameEn = "Non-binary", NamePl = "Niebinarny" },
            new GenderOption { Id = 4, NameEn = "Other", NamePl = "Inne" }
        );

        modelBuilder.Entity<PronounOption>().HasData(
            new PronounOption { Id = 1, NameEn = "He/Him", NamePl = "On/Jego" },
            new PronounOption { Id = 2, NameEn = "She/Her", NamePl = "Ona/Jej" },
            new PronounOption { Id = 3, NameEn = "They/Them", NamePl = "Oni/Ich" }
        );

        modelBuilder.Entity<PersonalityTypeOption>().HasData(
            new PersonalityTypeOption { Id = 1, NameEn = "Introvert", NamePl = "Introwertyk" },
            new PersonalityTypeOption { Id = 2, NameEn = "Extrovert", NamePl = "Ekstrawertyk" },
            new PersonalityTypeOption { Id = 3, NameEn = "Ambivert", NamePl = "Ambiwertyk" }
        );

        modelBuilder.Entity<AlcoholPreferenceOption>().HasData(
            new AlcoholPreferenceOption { Id = 1, NameEn = "Does not drink but does not mind", NamePl = "Nie piję i nie przeszkadza mi" },
            new AlcoholPreferenceOption { Id = 2, NameEn = "Social drinker", NamePl = "Piję okazjonalnie" },
            new AlcoholPreferenceOption { Id = 3, NameEn = "Regular drinker", NamePl = "Piję regularnie" },
            new AlcoholPreferenceOption { Id = 4, NameEn = "Does not tolerate alcohol", NamePl = "Nie toleruję" }
        );

        modelBuilder.Entity<SmokingPreferenceOption>().HasData(
            new SmokingPreferenceOption { Id = 1, NameEn = "Non-smoker but does not mind", NamePl = "Nie palę i nie przeszkadza mi" },
            new SmokingPreferenceOption { Id = 2, NameEn = "Occasional smoker", NamePl = "Palę okazjonalnie" },
            new SmokingPreferenceOption { Id = 3, NameEn = "Smoker", NamePl = "Palę" },
            new SmokingPreferenceOption { Id = 4, NameEn = "Does not tolerate smoking", NamePl = "Nie toleruję" }
        );

        modelBuilder.Entity<DrivingLicenseOption>().HasData(
            new DrivingLicenseOption { Id = 1, NameEn = "Yes, international", NamePl = "Posiadam międzynarodowe" },
            new DrivingLicenseOption { Id = 2, NameEn = "No", NamePl = "Nie posiadam" },
            new DrivingLicenseOption { Id = 3, NameEn = "Other", NamePl = "Inne" }
        );

        modelBuilder.Entity<TravelExperienceOption>().HasData(
            new TravelExperienceOption { Id = 1, NameEn = "Beginner", NamePl = "Początkujący" },
            new TravelExperienceOption { Id = 2, NameEn = "Intermediate", NamePl = "Średniozaawansowany" },
            new TravelExperienceOption { Id = 3, NameEn = "Experienced", NamePl = "Zaawansowany" },
            new TravelExperienceOption { Id = 4, NameEn = "Backpacker", NamePl = "Backpacker" }
        );

        modelBuilder.Entity<Models.TransportMode>().HasData(
            new Models.TransportMode { IdTransportMode = 1, TransportModeNameEn = "Car", TransportModeNamePl = "Samochód" },
            new Models.TransportMode { IdTransportMode = 2, TransportModeNameEn = "Plane", TransportModeNamePl = "Samolot" },
            new Models.TransportMode { IdTransportMode = 3, TransportModeNameEn = "Train", TransportModeNamePl = "Pociąg" },
            new Models.TransportMode { IdTransportMode = 4, TransportModeNameEn = "Bus", TransportModeNamePl = "Autobus" },
            new Models.TransportMode { IdTransportMode = 5, TransportModeNameEn = "Bike", TransportModeNamePl = "Rower" },
            new Models.TransportMode { IdTransportMode = 6, TransportModeNameEn = "Motorbike", TransportModeNamePl = "Motor" },
            new Models.TransportMode { IdTransportMode = 7, TransportModeNameEn = "Ferry", TransportModeNamePl = "Prom" }
        );
        
        modelBuilder.Entity<Models.TravelStyle>().HasData(
            new Models.TravelStyle { IdTravelStyle = 1, TravelStyleNameEn = "Spontaneous", TravelStyleNamePl = "Spontaniczny" },
            new Models.TravelStyle { IdTravelStyle = 2, TravelStyleNameEn = "Partially planned", TravelStyleNamePl = "Trochę zaplanowany" },
            new Models.TravelStyle { IdTravelStyle = 3, TravelStyleNameEn = "Fully planned", TravelStyleNamePl = "Szczegółowo zaplanowany" },
            new Models.TravelStyle { IdTravelStyle = 4, TravelStyleNameEn = "All-inclusive", TravelStyleNamePl = "All-inclusive" },
            new Models.TravelStyle { IdTravelStyle = 5, TravelStyleNameEn = "City break", TravelStyleNamePl = "City break" },
            new Models.TravelStyle { IdTravelStyle = 6, TravelStyleNameEn = "Road trip", TravelStyleNamePl = "Road trip" },
            new Models.TravelStyle { IdTravelStyle = 7, TravelStyleNameEn = "Package holiday", TravelStyleNamePl = "Wakacje z biurem podróży" },
            new Models.TravelStyle { IdTravelStyle = 8, TravelStyleNameEn = "Extreme travel", TravelStyleNamePl = "Podróże ekstremalne" },
            new Models.TravelStyle { IdTravelStyle = 9, TravelStyleNameEn = "Slow travel", TravelStyleNamePl = "Slow travel" },
            new Models.TravelStyle { IdTravelStyle = 10, TravelStyleNameEn = "Backpacking", TravelStyleNamePl = "Backpacking" }
        );
    }
}