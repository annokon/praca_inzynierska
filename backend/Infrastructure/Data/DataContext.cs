using backend.Models;
using backend.CategoriesOptions.Models;
using backend.Interest;
using backend.Language;
using backend.TransportMode;
using backend.TravelStyle;
using backend.User.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User.Models.User> Users => Set<User.Models.User>();
    public DbSet<Language.Language> Languages => Set<Language.Language>();
    public DbSet<UserLanguage> UserLanguages => Set<UserLanguage>();
    public DbSet<Interest.Interest> Interests => Set<Interest.Interest>();
    public DbSet<TravelStyle.TravelStyle> TravelStyles => Set<TravelStyle.TravelStyle>();
    public DbSet<TransportMode.TransportMode> TransportModes => Set<TransportMode.TransportMode>();
    public DbSet<GenderOption> GenderOptions => Set<GenderOption>();
    public DbSet<PronounOption> PronounOptions => Set<PronounOption>();
    public DbSet<PersonalityTypeOption> PersonalityTypeOptions => Set<PersonalityTypeOption>();
    public DbSet<AlcoholPreferenceOption> AlcoholPreferenceOptions => Set<AlcoholPreferenceOption>();
    public DbSet<SmokingPreferenceOption> SmokingPreferenceOptions => Set<SmokingPreferenceOption>();
    public DbSet<DrivingLicenseOption> DrivingLicenseOptions => Set<DrivingLicenseOption>();
    public DbSet<TravelExperienceOption> TravelExperienceOptions => Set<TravelExperienceOption>();
    public DbSet<Favourite> Favourites => Set<Favourite>();
    public DbSet<BlockedUser> BlockedUsers => Set<BlockedUser>();
    public DbSet<Notification.Notification> Notifications => Set<Notification.Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification.Notification>(entity =>
        {
            entity.HasOne(n => n.TripInvitation)
                .WithOne(ti => ti.Notification)
                .HasForeignKey<Notification.Notification>(n => n.IdTripInvitation);

            entity.HasOne(n => n.Message)
                .WithMany(m => m.Notifications)
                .HasForeignKey(n => n.IdMessage)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // modelBuilder.Entity<BlockedUser>(entity =>
        // {
        //     entity.HasKey(b => new { b.IdUserBlocker, b.IdUserBlocked });
        //
        //     entity.HasOne(b => b.Blocker)
        //         .WithMany(u => u.BlockedUsers)
        //         .HasForeignKey(b => b.IdUserBlocker)
        //         .OnDelete(DeleteBehavior.Cascade);
        //
        //     entity.HasOne(b => b.Blocked)
        //         .WithMany(u => u.UsersBlockingThisUser)
        //         .HasForeignKey(b => b.IdUserBlocked)
        //         .OnDelete(DeleteBehavior.Cascade);
        // });

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
            new AlcoholPreferenceOption
                { IdAlcoholPreference = 1, AlcoholPreferenceName = "Nie piję i nie przeszkadza mi" },
            new AlcoholPreferenceOption { IdAlcoholPreference = 2, AlcoholPreferenceName = "Piję okazjonalnie" },
            new AlcoholPreferenceOption { IdAlcoholPreference = 3, AlcoholPreferenceName = "Piję regularnie" },
            new AlcoholPreferenceOption { IdAlcoholPreference = 4, AlcoholPreferenceName = "Nie toleruję" }
        );

        modelBuilder.Entity<SmokingPreferenceOption>().HasData(
            new SmokingPreferenceOption
                { IdSmokingPreference = 1, SmokingPreferenceName = "Nie palę i nie przeszkadza mi" },
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

        modelBuilder.Entity<TransportMode.TransportMode>().HasData(
            new TransportMode.TransportMode { IdTransportMode = 1, TransportModeName = "Samochód" },
            new TransportMode.TransportMode { IdTransportMode = 2, TransportModeName = "Samolot" },
            new TransportMode.TransportMode { IdTransportMode = 3, TransportModeName = "Pociąg" },
            new TransportMode.TransportMode { IdTransportMode = 4, TransportModeName = "Autobus" },
            new TransportMode.TransportMode { IdTransportMode = 5, TransportModeName = "Rower" },
            new TransportMode.TransportMode { IdTransportMode = 6, TransportModeName = "Motor" },
            new TransportMode.TransportMode { IdTransportMode = 7, TransportModeName = "Prom" }
        );

        modelBuilder.Entity<TravelStyle.TravelStyle>().HasData(
            new TravelStyle.TravelStyle { IdTravelStyle = 1, TravelStyleName = "Spontaniczny" },
            new TravelStyle.TravelStyle { IdTravelStyle = 2, TravelStyleName = "Trochę zaplanowany" },
            new TravelStyle.TravelStyle { IdTravelStyle = 3, TravelStyleName = "Szczegółowo zaplanowany" },
            new TravelStyle.TravelStyle { IdTravelStyle = 4, TravelStyleName = "All-inclusive" },
            new TravelStyle.TravelStyle { IdTravelStyle = 5, TravelStyleName = "City break" },
            new TravelStyle.TravelStyle { IdTravelStyle = 6, TravelStyleName = "Road trip" },
            new TravelStyle.TravelStyle { IdTravelStyle = 7, TravelStyleName = "Wakacje z biurem podróży" },
            new TravelStyle.TravelStyle { IdTravelStyle = 8, TravelStyleName = "Podróże ekstremalne" },
            new TravelStyle.TravelStyle { IdTravelStyle = 9, TravelStyleName = "Slow travel" },
            new TravelStyle.TravelStyle { IdTravelStyle = 10, TravelStyleName = "Backpacking" }
        );
        
        modelBuilder.HasPostgresExtension("pg_trgm");
        
        modelBuilder.Entity<User.Models.User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();

            entity.HasIndex(u => u.Username)
                .IsUnique()
                .HasDatabaseName("ix_user_username_unique");

            entity.HasIndex(u => u.Username)
                .HasMethod("gin")
                .HasOperators("gin_trgm_ops")
                .HasDatabaseName("ix_user_username_trgm")
                .IsUnique(false);

            entity.HasIndex(u => u.DisplayName)
                .HasMethod("gin")
                .HasOperators("gin_trgm_ops")
                .HasDatabaseName("ix_user_displayname_trgm");
        });
    }
}