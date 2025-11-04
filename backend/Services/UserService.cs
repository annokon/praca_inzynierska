using backend.DTOs;
using backend.DTOs.UserDTOs;
using backend.Interfaces;
using backend.Models;
using backend.Security;

namespace backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, PasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }


    // get all users
    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDTO
        {
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
    public async Task<UserDTO?> GetByUsernameAsync(string username)
    {
        var u = await _userRepository.GetByUsernameAsync(username);
        if (u == null) return null;

        return new UserDTO
        {
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

        var user = new User
        {
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
            Username = user.Username,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Gender = user.Gender,
            IsActive = user.IsActive,
            Role = user.Role
        };
    }

    // adding optional data to user during registration
    public async Task<bool> AddAdditionalDataAsync(string username, AdditionalDataUserDTO dto)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
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
    public async Task<bool> UpdateAsync(string username, UpdateUserDTO dto)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
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
    public async Task<bool> DeleteAsync(string username)
    {
        //TODO
        return true;
    }
}