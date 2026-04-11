using backend.Interest;
using backend.Language;
using backend.Security;
using backend.TransportMode;
using backend.TravelStyle;
using backend.User.DTOs;
using backend.User.Repositories;

namespace backend.User.Services;

public class UserService(
    IUserRepository userRepository,
    PasswordHasher passwordHasher,
    JwtService jwt)
    : IUserService
{
    // get all users
    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var users = await userRepository.GetAllAsync();
        return users.Select(u => new UserDTO
        {
            IdUser = u.IdUser,
            Username = u.Username,
            DisplayName = u.DisplayName,
            Email = u.Email,
            BirthDate = u.BirthDate,
            Gender = u.Gender.GenderName,
            Location = u.Location,
            PersonalityType = u.PersonalityType?.PersonalityTypeName,
            Bio = u.Bio,
            ProfilePhotoPath = u.ProfilePhotoPath,
            Role = u.Role,
            IsActive = u.IsActive
        });
    }

    // get user by username
    public async Task<UserDTO?> GetByIdUserAsync(int idUser)
    {
        var u = await userRepository.GetByIdUserAsync(idUser);
        if (u == null) return null;

        return new UserDTO
        {
            IdUser = u.IdUser,
            Username = u.Username,
            DisplayName = u.DisplayName,
            Email = u.Email,
            Gender = u.Gender.GenderName,
            Location = u.Location,
            PersonalityType = u.PersonalityType?.PersonalityTypeName,
            Bio = u.Bio,
            ProfilePhotoPath = u.ProfilePhotoPath,
            Role = u.Role,
            IsActive = u.IsActive
        };
    }

    // create new user
    public async Task<UserDTO> CreateAsync(CreateUserDTO dto)
    {
        if (await userRepository.ExistsByEmailAsync(dto.Email))
            throw new Exception("This email is already in use.");
        if (await userRepository.ExistsByUsernameAsync(dto.Username))
            throw new Exception("This username is already taken.");
        if (await userRepository.ValidateGender(dto.GenderId))
            throw new Exception("Invalid gender option.");

        var user = new User
        {
            IdUser = dto.IdUser,
            Username = dto.Username,
            DisplayName = dto.DisplayName,
            Email = dto.Email,
            PasswordHash = passwordHasher.HashPassword(dto.Password),
            BirthDate = dto.BirthDate,
            GenderId = dto.GenderId,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            Role = "user"
        };

        await userRepository.AddAsync(user);

        return new UserDTO
        {
            IdUser = user.IdUser,
            Username = user.Username,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Gender = user.Gender.GenderName,
            IsActive = user.IsActive,
            Role = user.Role
        };
    }

    // adding optional data to user during registration
    public async Task<bool> AddAdditionalDataAsync(int idUser, AdditionalDataUserDTO dto)
    {
        var user = await userRepository.GetUserWithRelationsAsync(idUser);
        if (user == null) return false;

        user.GenderId = dto.GenderId;
        user.PronounsId = dto.PronounsId;
        user.Location = dto.Location;
        user.PersonalityTypeId = dto.PersonalityTypeId;
        user.AlcoholPreferenceId = dto.AlcoholPreferenceId;
        user.SmokingPreferenceId = dto.SmokingPreferenceId;
        user.DrivingLicenseId = dto.DrivingLicenseTypeId;
        user.TravelExperienceId = dto.TravelExperienceId;

        user.UserLanguages?.Clear();
        foreach (var langId in dto.LanguageIds)
        {
            user.UserLanguages!.Add(new UserLanguage
            {
                IdUser = idUser,
                IdLanguage = langId
            });
        }

        user.UserInterests?.Clear();
        foreach (var interestId in dto.InterestIds)
        {
            user.UserInterests!.Add(new UserInterest
            {
                IdUser = idUser,
                IdInterest = interestId
            });
        }

        user.UserTravelStyles?.Clear();
        foreach (var styleId in dto.TravelStyleIds)
        {
            user.UserTravelStyles!.Add(new UserTravelStyle
            {
                IdUser = idUser,
                IdTravelStyle = styleId
            });
        }

        user.UserTransportModes?.Clear();
        foreach (var modeId in dto.TransportModeIds)
        {
            user.UserTransportModes!.Add(new UserTransportMode
            {
                IdUser = idUser,
                IdTransportMode = modeId
            });
        }

        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.UpdateAsync(user);

        return true;
    }

    // update user data
    public async Task<bool> UpdateAsync(int idUser, UpdateUserDTO dto)
    {
        var user = await userRepository.GetByIdUserAsync(idUser);
        if (user == null) return false;

        user.DisplayName = dto.DisplayName ?? user.DisplayName;
        user.Email = dto.Email ?? user.Email;
        user.Location = dto.Location ?? user.Location;
        user.Gender.GenderName = dto.Gender ?? user.Gender.GenderName;
        user.Bio = dto.Bio ?? user.Bio;
        user.PersonalityType.PersonalityTypeName = dto.PersonalityType ?? user.PersonalityType.PersonalityTypeName;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.UpdateAsync(user);
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
        if (dto == null)
            return RegisterResult.Fail("Brak danych.");
        
        var email = dto.Email?.Trim();
        var username = dto.Username?.Trim();
        var displayName = dto.DisplayName?.Trim();
        var password = dto.Password;
        
        if (string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(displayName) ||
            string.IsNullOrWhiteSpace(password))
            return RegisterResult.Fail("Wszystkie pola są wymagane.");
        
        if (username.Length < 3 || username.Length > 20)
            return RegisterResult.Fail("Nazwa użytkownika musi mieć 3–20 znaków.");

        if (displayName.Length < 3 || displayName.Length > 30)
            return RegisterResult.Fail("Wyświetlana nazwa musi mieć 3–30 znaków.");
        
        if (!System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-z0-9._-]+$"))
            return RegisterResult.Fail("Nazwa użytkownika zawiera niedozwolone znaki.");
        
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
        }
        catch
        {
            return RegisterResult.Fail("Niepoprawny adres email.");
        }
        
        if (await userRepository.ExistsByEmailAsync(email))
            return RegisterResult.Fail("Ten email jest już zajęty.");

        if (await userRepository.ExistsByUsernameAsync(username))
            return RegisterResult.Fail("Ta nazwa użytkownika jest już zajęta.");
        
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (dto.BirthDate > today)
            return RegisterResult.Fail("Data urodzenia nie może być w przyszłości.");

        if (dto.BirthDate.Year < 1900)
            return RegisterResult.Fail("Nieprawidłowa data urodzenia.");

        int age = today.Year - dto.BirthDate.Year;
        if (dto.BirthDate > today.AddYears(-age)) age--;

        if (age < 16)
            return RegisterResult.Fail("Musisz mieć co najmniej 16 lat.");
        
        if (password.Length < 8)
            return RegisterResult.Fail("Hasło musi mieć co najmniej 8 znaków.");

        if (!password.Any(char.IsUpper))
            return RegisterResult.Fail("Hasło musi zawierać wielką literę.");

        if (!password.Any(char.IsLower))
            return RegisterResult.Fail("Hasło musi zawierać małą literę.");

        if (!password.Any(char.IsDigit))
            return RegisterResult.Fail("Hasło musi zawierać cyfrę.");
        
        var user = new User
        {
            Username = username,
            DisplayName = displayName,
            Email = email,
            BirthDate = dto.BirthDate,
            PasswordHash = passwordHasher.HashPassword(password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Role = "user",
            Currency = "PLN",
            SystemLanguage = "pl-PL",
            IsActive = true
        };

        await userRepository.AddAsync(user);

        return RegisterResult.SuccessResult(user);
    }

    public async Task<LoginResult> LoginAsync(LoginUserDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Login) ||
            string.IsNullOrWhiteSpace(dto.Password))
            return LoginResult.Fail("Enter username or password.");

        var login = dto.Login.Trim();

        var user = await userRepository.GetByEmailOrUsernameAsync(login);

        if (user == null)
            return LoginResult.Fail("Incorrect login or password.");

        if (!passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            return LoginResult.Fail("Incorrect login or password.");

        return LoginResult.SuccessResult(
            access: jwt.GenerateAccessToken(user),
            refresh: jwt.GenerateRefreshToken(),
            role: user.Role
        );
    }


    public async Task<UserDTO?> GetByIdAsync(int id)
    {
        var u = await userRepository.GetByIdUserAsync(id);
        if (u == null) return null;

        return new UserDTO
        {
            IdUser = u.IdUser,
            Username = u.Username,
            DisplayName = u.DisplayName,
            Email = u.Email,
            Gender = u.Gender.GenderName,
            Location = u.Location,
            ProfilePhotoPath = u.ProfilePhotoPath,
            Role = u.Role
        };
    }


    public async Task<UserProfileDTO?> GetProfileAsync(int id)
    {
        var user = await userRepository.GetUserWithRelationsAsync(id);
        if (user == null) return null;

        return new UserProfileDTO
        {
            Id = user.IdUser,
            Username = user.Username,
            DisplayName = user.DisplayName,
            Email = user.Email,
            BirthDate = user.BirthDate.ToString("yyyy-MM-dd"),

            Gender = user.Gender?.GenderName,
            Pronouns = user.Pronouns?.PronounName,
            Personality = user.PersonalityType?.PersonalityTypeName,
            Location = user.Location,
            Bio = user.Bio,

            Languages = user.UserLanguages?
                .Where(ul => ul.Language != null)
                .Select(ul => ul.Language.LanguageName)
                .ToList() ?? new List<string>(),

            Interests = user.UserInterests?
                .Where(ui => ui.Interest != null)
                .Select(ui => ui.Interest.InterestName)
                .ToList() ?? new List<string>(),

            Transport = user.UserTransportModes?
                .Where(ut => ut.TransportMode != null)
                .Select(ut => ut.TransportMode.TransportModeName)
                .ToList() ?? new List<string>(),

            TravelStyles = user.UserTravelStyles?
                .Where(ts => ts.TravelStyle != null)
                .Select(ts => ts.TravelStyle.TravelStyleName)
                .ToList() ?? new List<string>(),

            TravelExperience = user.TravelExperience?.TravelExperienceName,
            DrivingLicense = user.DrivingLicense?.DrivingLicenseName,
            Alcohol = user.AlcoholPreference?.AlcoholPreferenceName,
            Smoking = user.SmokingPreference?.SmokingPreferenceName,
        };
    }

    public async Task<(bool Success, string? Error, UserDTO? User)> UpdateDisplayNameAsync(int userId, string displayName)
    {
        var user = await userRepository.GetByIdUserAsync(userId);

        if (user == null)
            return (false, "User not found.", null);

        displayName = displayName?.Trim();

        if (string.IsNullOrWhiteSpace(displayName))
            return (false, "Wyświetlana nazwa jest wymagana.", null);

        if (displayName.Length < 3 || displayName.Length > 30)
            return (false, "Wyświetlana nazwa musi mieć 3–30 znaków.", null);
        
        if (!System.Text.RegularExpressions.Regex.IsMatch(displayName, @"^[a-zA-Z0-9ąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s._-]+$"))
            return (false, "Wyświetlana nazwa zawiera niedozwolone znaki.", null);

        if (user.DisplayName.Equals(displayName, StringComparison.Ordinal))
            return (false, "Nowa nazwa jest taka sama jak obecna.", null);

        user.DisplayName = displayName;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.SaveChangesAsync();

        return (true, null, new UserDTO
        {
            IdUser = user.IdUser,
            Username = user.Username,
            DisplayName = user.DisplayName
        });
    }

    public async Task<(bool Success, string? Error, UserDTO? User)> UpdateUsernameAsync(int userId, string username)
    {
        var user = await userRepository.GetByIdUserAsync(userId);

        if (user == null)
            return (false, "User not found.", null);

        username = username?.Trim().ToLower() ?? throw new InvalidOperationException();

        if (string.IsNullOrWhiteSpace(username))
            return (false, "Nazwa użytkownika jest wymagana.", null);

        if (username.Length < 3 || username.Length > 20)
            return (false, "Nazwa użytkownika musi mieć 3–20 znaków.", null);

        if (!System.Text.RegularExpressions.Regex.IsMatch(username, @"^[a-z0-9._-]+$"))
            return (false, "Nazwa użytkownika zawiera niedozwolone znaki.", null);

        if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
            return (false, "Nowa nazwa użytkownika jest taka sama jak obecna.", null);

        var exists = await userRepository.ExistsByUsernameAsync(username);
        if (exists)
            return (false, "Ta nazwa użytkownika jest już zajęta.", null);

        user.Username = username;
        user.UpdatedAt = DateTime.UtcNow;

        await userRepository.SaveChangesAsync();

        return (true, null, new UserDTO
        {
            IdUser = user.IdUser,
            Username = user.Username,
            DisplayName = user.DisplayName
        });
    }

    public async Task<(bool Success, string? Error, UserDTO? User)> UpdateBirthDateAsync(int userId, DateOnly birthDate)
    {
        var user = await userRepository.GetByIdUserAsync(userId);

        if (user == null)
            return (false, "User not found.", null);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (birthDate > today)
            return (false, "Data urodzenia nie może być w przyszłości.", null);

        int age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age)) age--;

        if (age < 16)
            return (false, "Musisz mieć co najmniej 16 lat.", null);

        if (birthDate.Year < 1900)
            return (false, "Nieprawidłowa data urodzenia.", null);

        if (user.BirthDate == birthDate)
            return (false, "Nowa data urodzenia jest taka sama jak obecna.", null);

        user.BirthDate = birthDate;

        await userRepository.SaveChangesAsync();

        return (true, null, new UserDTO
        {
            IdUser = user.IdUser,
            Username = user.Username,
            DisplayName = user.DisplayName,
            BirthDate = user.BirthDate
        });
    }
    
    
}