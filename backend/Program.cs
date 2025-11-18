using Microsoft.EntityFrameworkCore;
using backend.Infrastructure.Data;
using backend.Repositories;
using backend.Services;
using backend.Interfaces;
using backend.Security;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<PasswordHasher>();

var app = builder.Build();


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