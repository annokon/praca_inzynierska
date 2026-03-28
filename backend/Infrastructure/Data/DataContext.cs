using backend.Models;
using backend.CategoriesOptions.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Models.User> Users => Set<Models.User>();
    public DbSet<Language.Language> Languages => Set<Language.Language>();
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
            new GenderOption { IdGender = 1, GenderName = "Mężczyzna" },
            new GenderOption { IdGender = 2, GenderName = "Kobieta" },
            new GenderOption { IdGender = 3, GenderName = "Niebinarny" },
            new GenderOption { IdGender = 4, GenderName = "Inne" }
        );

        modelBuilder.Entity<PronounOption>().HasData(
            new PronounOption { IdPronoun = 1, PronounName = "On/Jego" },
            new PronounOption { IdPronoun = 2, PronounName = "Ona/Jej" },
            new PronounOption { IdPronoun = 3, PronounName = "Oni/Ich" }
        );

        modelBuilder.Entity<PersonalityTypeOption>().HasData(
            new PersonalityTypeOption { IdPersonalityType = 1, PersonalityTypeName = "Introwertyk" },
            new PersonalityTypeOption { IdPersonalityType = 2, PersonalityTypeName = "Ekstrawertyk" },
            new PersonalityTypeOption { IdPersonalityType = 3, PersonalityTypeName = "Ambiwertyk" }
        );

        modelBuilder.Entity<AlcoholPreferenceOption>().HasData(
            new AlcoholPreferenceOption { IdAlcoholPreference = 1, AlcoholPreferenceName = "Nie piję i nie przeszkadza mi" },
            new AlcoholPreferenceOption { IdAlcoholPreference = 2, AlcoholPreferenceName = "Piję okazjonalnie" },
            new AlcoholPreferenceOption { IdAlcoholPreference = 3, AlcoholPreferenceName = "Piję regularnie" },
            new AlcoholPreferenceOption { IdAlcoholPreference = 4, AlcoholPreferenceName = "Nie toleruję" }
        );

        modelBuilder.Entity<SmokingPreferenceOption>().HasData(
            new SmokingPreferenceOption { IdSmokingPreference = 1, SmokingPreferenceName = "Nie palę i nie przeszkadza mi" },
            new SmokingPreferenceOption { IdSmokingPreference = 2, SmokingPreferenceName = "Palę okazjonalnie" },
            new SmokingPreferenceOption { IdSmokingPreference = 3, SmokingPreferenceName = "Palę" },
            new SmokingPreferenceOption { IdSmokingPreference = 4, SmokingPreferenceName = "Nie toleruję" }
        );

        modelBuilder.Entity<DrivingLicenseOption>().HasData(
            new DrivingLicenseOption { IdDrivingLicense = 1, DrivingLicenseName = "Posiadam międzynarodowe" },
            new DrivingLicenseOption { IdDrivingLicense = 2, DrivingLicenseName = "Nie posiadam" },
            new DrivingLicenseOption { IdDrivingLicense = 3, DrivingLicenseName = "Inne" }
        );

        modelBuilder.Entity<TravelExperienceOption>().HasData(
            new TravelExperienceOption { IdTravelExperience = 1, TravelExperienceName = "Początkujący" },
            new TravelExperienceOption { IdTravelExperience = 2, TravelExperienceName = "Średniozaawansowany" },
            new TravelExperienceOption { IdTravelExperience = 3, TravelExperienceName = "Zaawansowany" },
            new TravelExperienceOption { IdTravelExperience = 4, TravelExperienceName = "Backpacker" }
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