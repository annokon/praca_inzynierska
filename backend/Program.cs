using Microsoft.EntityFrameworkCore;
using backend.Infrastructure.Data;
using backend.User;
using backend.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend.CategoriesOptions.Repositories;
using backend.CategoriesOptions.Services;
using backend.Interest;
using backend.Interest.Repositories;
using backend.Interest.Services;
using backend.Language;
using backend.Language.Repositories;
using backend.Language.Services;
using backend.TransportMode;
using backend.TransportMode.Repositories;
using backend.TransportMode.Services;
using backend.TravelStyle;
using backend.TravelStyle.Repositories;
using backend.TravelStyle.Services;
using backend.User.Models;
using backend.User.Repositories;
using backend.User.Services;


var builder = WebApplication.CreateBuilder(args);
var jwtConfig = builder.Configuration.GetSection("Jwt");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<IEmailVerificationRepository, EmailVerificationRepository>();
builder.Services.AddScoped<IEmailVerificationService, EmailVerificationService>();
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("Email"));
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<IInterestRepository, InterestRepository>();
builder.Services.AddScoped<IInterestService, InterestService>();
builder.Services.AddScoped<ITransportModeRepository, TransportModeRepository>();
builder.Services.AddScoped<ITransportModeService, TransportModeService>();
builder.Services.AddScoped<ITravelStyleRepository, TravelStyleRepository>();
builder.Services.AddScoped<ITravelStyleService, TravelStyleService>();
builder.Services.AddScoped<IOptionsService, OptionsService>();
builder.Services.AddScoped<IOptionsRepository, OptionsRepository>();
builder.Services.AddScoped<IFavouriteRepository, FavouriteRepository>();
builder.Services.AddScoped<IFavouriteService, FavouriteService>();
builder.Services.AddScoped<IBlockedUserRepository, BlockedUserRepository>();
builder.Services.AddScoped<IBlockedUserService, BlockedUserService>();

builder.Services.AddHttpClient();

builder.Services.AddScoped<JwtService>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]))
        };
        
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                ctx.Token = ctx.Request.Cookies["access_token"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();


var placeholdersSource = Path.Combine(
    app.Environment.ContentRootPath,
    "Resources",
    "Placeholders"
);

var placeholdersTarget = Path.Combine(
    app.Environment.WebRootPath,
    "images",
    "placeholders"
);

Directory.CreateDirectory(placeholdersTarget);

var files = new[]
{
    "profile_picture.png",
    "banner_picture.png"
};

foreach (var file in files)
{
    var sourcePath = Path.Combine(placeholdersSource, file);
    var targetPath = Path.Combine(placeholdersTarget, file);

    if (!File.Exists(targetPath) && File.Exists(sourcePath))
    {
        File.Copy(sourcePath, targetPath);
    }
}

// sample data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    var hasher = scope.ServiceProvider.GetRequiredService<PasswordHasher>();
    var languageService = scope.ServiceProvider.GetRequiredService<ILanguageService>();
    
    await languageService.SeedLanguagesAsync();
    Console.WriteLine("Languages imported from CLDR JSON files.");
    
    
    var interestService = scope.ServiceProvider.GetRequiredService<IInterestService>();
    await interestService.SeedInterestsAsync();

    
    if (!db.Users.Any())
    {
        var user1 = new User
        {
            Username = "testuser",
            DisplayName = "Test User",
            Email = "test@example.com",
            PasswordHash = hasher.HashPassword("Test123!"),

            BirthDate = new DateOnly(2000, 1, 1),

            GenderId = 1,
            PronounsId = 1,
            PersonalityTypeId = 1,
            AlcoholPreferenceId = 1,
            SmokingPreferenceId = 1,
            DrivingLicenseId = 1,
            TravelExperienceId = 1,

            Location = "Warszawa, województwo mazowieckie, Polska",
            Bio = "To jest przykładowy użytkownik testowy",

            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,

            Role = "user",
            IsActive = true,

            Currency = "PLN",
            
            ProfilePhotoPath = "/images/placeholders/profile_picture.png",
            BackgroundPhotoPath = "/images/placeholders/banner_picture.png",
            
            UserLanguages = new List<UserLanguage>
            {
                new UserLanguage { IdLanguage = 1 },
                new UserLanguage { IdLanguage = 2 }
            },

            UserInterests = new List<UserInterest>
            {
                new UserInterest { IdInterest = 1 },
                new UserInterest { IdInterest = 2 },
                new UserInterest { IdInterest = 3 }
            },

            UserTravelStyles = new List<UserTravelStyle>
            {
                new UserTravelStyle { IdTravelStyle = 1 },
                new UserTravelStyle { IdTravelStyle = 2 }
            },

            UserTransportModes = new List<UserTransportMode>
            {
                new UserTransportMode { IdTransportMode = 1 },
                new UserTransportMode { IdTransportMode = 2 }
            }
        };
        
        var user2 = new User
        {
            Username = "testuser2",
            DisplayName = "Second User",
            Email = "test2@example.com",
            PasswordHash = hasher.HashPassword("Test123!"),

            BirthDate = new DateOnly(1998, 5, 15),

            GenderId = 2,
            PronounsId = 2,
            PersonalityTypeId = 2,
            AlcoholPreferenceId = 2,
            SmokingPreferenceId = 2,
            DrivingLicenseId = 2,
            TravelExperienceId = 2,

            Location = "Kraków, województwo małopolskie, Polska",
            Bio = "Drugi użytkownik testowy",

            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,

            Role = "user",
            IsActive = true,

            Currency = "PLN",

            ProfilePhotoPath = "/images/placeholders/profile_picture.png",
            BackgroundPhotoPath = "/images/placeholders/banner_picture.png",

            UserLanguages = new List<UserLanguage>
            {
                new UserLanguage { IdLanguage = 1 }
            },

            UserInterests = new List<UserInterest>
            {
                new UserInterest { IdInterest = 2 },
                new UserInterest { IdInterest = 4 }
            },

            UserTravelStyles = new List<UserTravelStyle>
            {
                new UserTravelStyle { IdTravelStyle = 3 }
            },

            UserTransportModes = new List<UserTransportMode>
            {
                new UserTransportMode { IdTransportMode = 3 }
            }
        };

        db.Users.AddRange(user1, user2);
        db.SaveChanges();
        Console.WriteLine("2 seed users added to database.");
    }
    else
    {
        Console.WriteLine("Database already has users. Seed skipped.");
    }
}

Console.WriteLine("Application started successfully. The server is up and running.");
app.Run();