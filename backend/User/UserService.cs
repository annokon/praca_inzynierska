using System.Text.Json;
using backend.DTOs;
using backend.DTOs.UserDTOs;
using backend.Models;
using backend.Security;
using backend.User;
using backend.User.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher _passwordHasher;
    private readonly JwtService _jwt;
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly string _apiKey;

    public UserService(IUserRepository userRepository, PasswordHasher passwordHasher, 
        JwtService jwt, HttpClient httpClient, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwt = jwt;
        _httpClient = httpClient;
        
        _apiUrl = configuration["ExternalApi:LanguagesApiUrl"]!;
        _apiKey = configuration["ExternalApi:ApiKey"]!;
    }


    // get all users
    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDTO
        {
            IdUser = u.IdUser,
            Username = u.Username,
            DisplayName = u.DisplayName,
            Email = u.Email,
            BirthDate = u.BirthDate,
            Gender = u.Gender,
            Location = u.Location,
            PersonalityType = u.PersonalityType,
            Bio = u.Bio,
            ProfilePhotoPath = u.ProfilePhotoPath,
            Role = u.Role,
            IsActive = u.IsActive
        });
    }

    // get user by username
    public async Task<UserDTO?> GetByIdUserAsync(int idUser)
    {
        var u = await _userRepository.GetByIdUserAsync(idUser);
        if (u == null) return null;

        return new UserDTO
        {
            IdUser = u.IdUser,
            Username = u.Username,
            DisplayName = u.DisplayName,
            Email = u.Email,
            Gender = u.Gender,
            Location = u.Location,
            PersonalityType = u.PersonalityType,
            Bio = u.Bio,
            ProfilePhotoPath = u.ProfilePhotoPath,
            Role = u.Role,
            IsActive = u.IsActive
        };
    }

    // create new user
    public async Task<UserDTO> CreateAsync(CreateUserDTO dto)
    {
        if (await _userRepository.ExistsByEmailAsync(dto.Email))
            throw new Exception("This email is already in use.");
        if (await _userRepository.ExistsByUsernameAsync(dto.Username))
            throw new Exception("This username is already taken.");

        var user = new backend.Models.User
        {
            IdUser = dto.IdUser,
            Username = dto.Username,
            DisplayName = dto.DisplayName,
            Email = dto.Email,
            PasswordHash = _passwordHasher.HashPassword(dto.Password),
            BirthDate = dto.BirthDate,
            Gender = dto.Gender,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            Role = "user"
        };

        await _userRepository.AddAsync(user);

        return new UserDTO
        {
            IdUser = user.IdUser,
            Username = user.Username,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Gender = user.Gender,
            IsActive = user.IsActive,
            Role = user.Role
        };
    }

    // adding optional data to user during registration
    public async Task<bool> AddAdditionalDataAsync(int idUser, AdditionalDataUserDTO dto)
    {
        var user = await _userRepository.GetByIdUserAsync(idUser);
        if (user == null) return false;

        user.Pronouns = dto.Pronouns;
        user.AlcoholPreference = dto.AlcoholPreference;
        user.SmokingPreference = dto.SmokingPreference;
        user.DrivingLicenseType = dto.DrivingLicenseType;
        user.TravelStyle = dto.TravelStyle;
        user.TravelExperience = dto.TravelExperience;
        user.PersonalityType = dto.PersonalityType;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
        return true;
    }

    // update user data
    public async Task<bool> UpdateAsync(int idUser, UpdateUserDTO dto)
    {
        var user = await _userRepository.GetByIdUserAsync(idUser);
        if (user == null) return false;

        user.DisplayName = dto.DisplayName ?? user.DisplayName;
        user.Email = dto.Email ?? user.Email;
        user.Location = dto.Location ?? user.Location;
        user.Gender = dto.Gender ?? user.Gender;
        user.Bio = dto.Bio ?? user.Bio;
        user.PersonalityType = dto.PersonalityType ?? user.PersonalityType;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
        return true;
    }

    // deleting user
    public async Task<bool> DeleteAsync(int idUser)
    {
        //TODO
        return true;
    }

    public async Task<RegisterResult> RegisterAsync(RegisterUserDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) ||
            string.IsNullOrWhiteSpace(dto.Username) ||
            string.IsNullOrWhiteSpace(dto.DisplayName) ||
            string.IsNullOrWhiteSpace(dto.Password))
            return RegisterResult.Fail("Wszystkie pola są wymagane.");

        if (!dto.Email.Contains('@') || !dto.Email.Contains('.'))
            return RegisterResult.Fail("Podaj poprawny adres email.");

        if (await _userRepository.ExistsByEmailAsync(dto.Email))
            return RegisterResult.Fail("Ten email jest już zajęty.");

        if (await _userRepository.ExistsByUsernameAsync(dto.Username))
            return RegisterResult.Fail("Ta nazwa użytkownika jest już zajęta.");

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        if (dto.BirthDate > today.AddYears(-16))
            return RegisterResult.Fail("Musisz mieć co najmniej 16 lat.");

        if (dto.Password.Length < 8)
            return RegisterResult.Fail("Hasło musi mieć co najmniej 8 znaków.");

        var user = new backend.Models.User
        {
            Username = dto.Username.Trim(),
            DisplayName = dto.DisplayName.Trim(),
            Email = dto.Email.Trim(),
            BirthDate = dto.BirthDate,
            PasswordHash = _passwordHasher.HashPassword(dto.Password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Role = "user",
            Currency = "PLN",
            SystemLanguage = "pl-PL",
            IsActive = true
        };

        await _userRepository.AddAsync(user);

        return RegisterResult.SuccessResult(user);
    }


    public async Task<LoginResult> LoginAsync(LoginUserDTO dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null)
            return LoginResult.Fail("Invalid credentials");

        if (!_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            return LoginResult.Fail("Invalid credentials");

        return LoginResult.SuccessResult(
            access: _jwt.GenerateAccessToken(user),
            refresh: _jwt.GenerateRefreshToken(),
            role: user.Role
        );
    }
    
    public async Task<UserDTO?> GetByIdAsync(int id)
    {
        var u = await _userRepository.GetByIdUserAsync(id);
        if (u == null) return null;

        return new UserDTO
        {
            IdUser = u.IdUser,
            Username = u.Username,
            DisplayName = u.DisplayName,
            Email = u.Email,
            Gender = u.Gender,
            Location = u.Location,
            ProfilePhotoPath = u.ProfilePhotoPath,
            Role = u.Role
        };
    }
    
    
    public async Task<UserProfileDTO?> GetProfileAsync(int id)
    {
        var user = await _userRepository.GetUserWithLanguagesAsync(id);
        if (user == null) return null;

        return new UserProfileDTO
        {
            Id = user.IdUser,
            Username = user.Username,
            DisplayName = user.DisplayName,
            Email = user.Email,
            BirthDate = user.BirthDate.ToString("yyyy-MM-dd"),
            Gender = user.Gender,
            Pronouns = user.Pronouns,
            Personality = user.PersonalityType,
            Location = user.Location,
            Bio = user.Bio,
            Languages = user.UserLanguages?
                .Select(ul => ul.Language.LanguageNamePL)
                .ToList() ?? []
        };
    }

}