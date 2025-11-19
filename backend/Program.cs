using Microsoft.EntityFrameworkCore;
using backend.Infrastructure.Data;
using backend.Services;
using backend.Security;
using backend.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var jwtConfig = builder.Configuration.GetSection("Jwt");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<PasswordHasher>();

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

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseHttpsRedirection();

// sample data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    var hasher = scope.ServiceProvider.GetRequiredService<PasswordHasher>();
    
    if (!db.Users.Any())
    {
        db.Users.Add(new backend.Models.User
        {
            Username = "testuser",
            DisplayName = "Test User",
            Email = "test@example.com",
            PasswordHash = hasher.HashPassword("Test123!"),
            BirthDate = new DateOnly(2000, 1, 1),
            Gender = "other",
            IsActive = true,
            Role = "user",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Currency = "PLN",
            SystemLanguage = "pl"
        });

        db.SaveChanges();
        Console.WriteLine("Seed data added to database.");
    }
    else
    {
        Console.WriteLine("Database already has users. Seed skipped.");
    }
}


app.Run();