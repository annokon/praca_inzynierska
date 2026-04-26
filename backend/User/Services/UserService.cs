using System.Net.Mail;
using System.Text.RegularExpressions;
using backend.Interest;
using backend.Language;
using backend.Security;
using backend.TransportMode;
using backend.TravelStyle;
using backend.User.DTOs;
using backend.User.Repositories;

namespace backend.User.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IBlockedUserRepository _blockedRepo;
    private readonly PasswordHasher _passwordHasher;
    private readonly JwtService _jwt;
    private readonly IWebHostEnvironment _env;
    private readonly string _currencyFilePath;
    private readonly string _placeholderProfilePath = "/images/placeholders/profile_picture.png";
    private readonly string _placeholderBannerPath = "/images/placeholders/banner_picture.png";

    public UserService(
        IUserRepository userRepository,
        PasswordHasher passwordHasher,
        JwtService jwt,
        IWebHostEnvironment env,
        IBlockedUserRepository blockedRepo
    )
    {
        _repo = userRepository;
        _blockedRepo = blockedRepo;
        _passwordHasher = passwordHasher;
        _jwt = jwt;
        _env = env;

        _currencyFilePath = Path.Combine(
            env.ContentRootPath,
            "Resources",
            "currency_list.txt"
        );
    }

    // get all users
    public async Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        var users = await _repo.GetAllAsync();
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

    // get user by id
    public async Task<UserDTO?> GetByIdAsync(int idUser, int? currentUserId)
    {
        var u = await _repo.GetByIdUserAsync(idUser);
        if (u == null) return null;
        
        if (currentUserId != null)
        {
            var isBlocked = await _blockedRepo.ExistsEitherWay(currentUserId.Value, idUser);

            if (isBlocked)
                return null;
        }

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
        if (await _repo.ExistsByEmailAsync(dto.Email))
            throw new Exception("This email is already in use.");
        if (await _repo.ExistsByUsernameAsync(dto.Username))
            throw new Exception("This username is already taken.");
        if (await _repo.ValidateGender(dto.GenderId))
            throw new Exception("Invalid gender option.");

        var user = new Models.User
        {
            IdUser = dto.IdUser,
            Username = dto.Username,
            DisplayName = dto.DisplayName,
            Email = dto.Email,
            PasswordHash = _passwordHasher.HashPassword(dto.Password),
            BirthDate = dto.BirthDate,
            GenderId = dto.GenderId,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            Role = "user"
        };

        await _repo.AddAsync(user);

        return new UserDTO
        {
            IdUser = user.IdUser,
            Username = user.Username,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Gender = user.Gender.GenderName,
            IsActive = user.IsActive,
            Role = user.Role,

            ProfilePhotoPath = _placeholderProfilePath,
            BackgroundPhotoPath = _placeholderBannerPath
        };
    }

    // adding optional data to user during registration
    public async Task<bool> AddAdditionalDataAsync(int idUser, AdditionalDataUserDTO dto)
    {
        var user = await _repo.GetUserWithRelationsAsync(idUser);
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

        await _repo.UpdateAsync(user);

        return true;
    }

    // update user data
    public async Task<bool> UpdateAsync(int idUser, UpdateUserDTO dto)
    {
        var user = await _repo.GetByIdUserAsync(idUser);
        if (user == null) return false;

        user.DisplayName = dto.DisplayName ?? user.DisplayName;
        user.Email = dto.Email ?? user.Email;
        user.Location = dto.Location ?? user.Location;
        user.Gender.GenderName = dto.Gender ?? user.Gender.GenderName;
        user.Bio = dto.Bio ?? user.Bio;
        user.PersonalityType.PersonalityTypeName = dto.PersonalityType ?? user.PersonalityType.PersonalityTypeName;
        user.UpdatedAt = DateTime.UtcNow;

        await _repo.UpdateAsync(user);
        return true;
    }

    // delete user
    public async Task<bool> DeleteAsync(int idUser)
    {
        //TODO
        return true;
    }

    // new user
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

        if (await _repo.ExistsByEmailAsync(email))
            return RegisterResult.Fail("Ten email jest już zajęty.");

        if (await _repo.ExistsByUsernameAsync(username))
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

        var user = new Models.User
        {
            Username = username,
            DisplayName = displayName,
            Email = email,
            BirthDate = dto.BirthDate,
            PasswordHash = _passwordHasher.HashPassword(password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Role = "user",
            Currency = "PLN",
            IsActive = true,

            ProfilePhotoPath = _placeholderProfilePath,
            BackgroundPhotoPath = _placeholderBannerPath
        };

        await _repo.AddAsync(user);

        return RegisterResult.SuccessResult(user);
    }

    // login
    public async Task<LoginResult> LoginAsync(LoginUserDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Login) ||
            string.IsNullOrWhiteSpace(dto.Password))
            return LoginResult.Fail("Enter username or password.");

        var login = dto.Login.Trim();

        var user = await _repo.GetByEmailOrUsernameAsync(login);

        if (user == null)
            return LoginResult.Fail("Incorrect login or password.");

        if (!_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            return LoginResult.Fail("Incorrect login or password.");

        return LoginResult.SuccessResult(
            access: _jwt.GenerateAccessToken(user),
            refresh: _jwt.GenerateRefreshToken(),
            role: user.Role
        );
    }

    // get user by id
    public async Task<UserDTO?> GetByIdAsync(int id)
    {
        var u = await _repo.GetByIdUserAsync(id);
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

    // get user
    public async Task<UserProfileDTO?> GetProfileByIdAsync(int targetUserId, int? currentUserId)
    {
        var user = await _repo.GetUserWithRelationsAsync(targetUserId);
        if (user == null) return null;

        if (currentUserId != null)
        {
            var isBlocked = await _blockedRepo.ExistsEitherWay(currentUserId.Value, targetUserId);

            if (isBlocked)
                return null;
        }

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
                .Select(ul => ul.Language.LanguageName)
                .ToList() ?? new List<string>(),

            Interests = user.UserInterests?
                .Select(ui => ui.Interest.InterestName)
                .ToList() ?? new List<string>(),

            Transport = user.UserTransportModes?
                .Select(ut => ut.TransportMode.TransportModeName)
                .ToList() ?? new List<string>(),

            TravelStyles = user.UserTravelStyles?
                .Select(ts => ts.TravelStyle.TravelStyleName)
                .ToList() ?? new List<string>(),

            TravelExperience = user.TravelExperience?.TravelExperienceName,
            DrivingLicense = user.DrivingLicense?.DrivingLicenseName,
            Alcohol = user.AlcoholPreference?.AlcoholPreferenceName,
            Smoking = user.SmokingPreference?.SmokingPreferenceName,

            Currency = user.Currency,

            IsMe = targetUserId == currentUserId
        };
    }

    // update user
    public async Task<(bool Success, string? Error, UserDTO? User)> UpdateProfileAsync(int? userId,
        UpdateUserProfileDTO dto)
    {
        var user = await _repo.GetUserWithRelationsAsync(userId);
        if (user == null)
            return (false, "Użytkownik nie został znaleziony.", null);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var hasAnyChange = false;

        // =========================
        // BASIC
        // =========================

        if (dto.DisplayName != null)
        {
            var name = dto.DisplayName.Trim();

            if (name.Length is < 3 or > 30)
                return (false, "Nazwa wyświetlana musi mieć od 3 do 30 znaków.", null);

            if (string.Equals(user.DisplayName.Trim(), name, StringComparison.OrdinalIgnoreCase))
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            if (!Regex.IsMatch(name, @"^[a-zA-Z0-9ąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s._-]+$"))
                return (false, "Wyświetlana nazwa zawiera niedozwolone znaki.", null);

            user.DisplayName = name;
            hasAnyChange = true;
        }

        if (dto.Username != null)
        {
            var username = dto.Username.Trim().ToLowerInvariant();

            if (!Regex.IsMatch(username, @"^[a-z0-9._-]+$"))
                return (false, "Nieprawidłowa nazwa użytkownika.", null);

            if (string.Equals(user.Username, username, StringComparison.OrdinalIgnoreCase))
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            if (username.Length is < 3 or > 20)
                return (false, "Nazwa użytkownika musi mieć 3–20 znaków.", null);

            if (!Regex.IsMatch(username, @"^[a-z0-9._-]+$"))
                return (false, "Nazwa użytkownika zawiera niedozwolone znaki.", null);

            var forbidden = new[] { "admin", "root", "support" };
            if (forbidden.Contains(username))
                return (false, "Zawiera niedozwolone słowa.", null);

            if (await _repo.ExistsByUsernameAsync(username))
                return (false, "Ta nazwa użytkownika jest już zajęta.", null);

            user.Username = username;
            hasAnyChange = true;
        }

        if (dto.Email != null)
        {
            var email = dto.Email.Trim().ToLowerInvariant();

            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                return (false, "Nieprawidłowy e-mail.", null);
            }

            if (string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            if (await _repo.ExistsByEmailAsync(email))
                return (false, "Ten e-mail jest już zajęty.", null);

            user.Email = email;
            hasAnyChange = true;
        }

        if (dto.BirthDate.HasValue)
        {
            var birth = dto.BirthDate.Value;

            if (birth > today)
                return (false, "Data urodzenia nie może być z przyszłości.", null);

            if (birth.Year < 1900)
                return (false, "Data urodzenia jest zbyt odległa.", null);

            var age = today.Year - birth.Year;
            if (birth > today.AddYears(-age)) age--;

            if (age < 16)
                return (false, "Musisz mieć co najmniej 16 lat.", null);

            if (user.BirthDate == birth)
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            user.BirthDate = birth;
            hasAnyChange = true;
        }

        // =========================
        // OPTIONAL STRINGS (-1 = delete)
        // =========================

        if (dto.Bio != null)
        {
            var bio = dto.Bio.Trim();
            var value = bio == "-1" || string.IsNullOrWhiteSpace(bio) ? null : bio;

            if (user.Bio == value)
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            user.Bio = value;
            hasAnyChange = true;
        }

        if (dto.Location != null)
        {
            var loc = dto.Location.Trim();
            var value = loc == "-1" || string.IsNullOrWhiteSpace(loc) ? null : loc;

            if (user.Location == value)
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            user.Location = value;
            hasAnyChange = true;
        }

        // =========================
        // OPTIONAL ENUMS 1:1 (-1 = delete)
        // =========================

        if (dto.GenderId != null)
        {
            var value = dto.GenderId == -1 ? null : dto.GenderId;

            if (user.GenderId == value)
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            if (value != null &&
                !await _repo.ValidateGender(value.Value))
                return (false, "Nieprawidłowa płeć.", null);

            user.GenderId = value;
            hasAnyChange = true;
        }

        if (dto.PronounsId != null)
        {
            var value = dto.PronounsId == -1 ? null : dto.PronounsId;

            if (user.PronounsId == value)
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            if (value != null &&
                !await _repo.ValidatePronouns(value.Value))
                return (false, "Nieprawidłowe zaimki.", null);

            user.PronounsId = value;
            hasAnyChange = true;
        }

        if (dto.PersonalityTypeId != null)
        {
            var value = dto.PersonalityTypeId == -1 ? null : dto.PersonalityTypeId;

            if (user.PersonalityTypeId == value)
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            if (value != null &&
                !await _repo.ValidatePersonalityType(value.Value))
                return (false, "Nieprawidłowy typ osobowości.", null);

            user.PersonalityTypeId = value;
            hasAnyChange = true;
        }

        if (dto.AlcoholPreferenceId != null)
        {
            var value = dto.AlcoholPreferenceId == -1 ? null : dto.AlcoholPreferenceId;

            if (user.AlcoholPreferenceId == value)
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            if (value != null &&
                !await _repo.ValidateAlcoholPreference(value.Value))
                return (false, "Nieprawidłowa preferencja alkoholu.", null);

            user.AlcoholPreferenceId = value;
            hasAnyChange = true;
        }

        if (dto.SmokingPreferenceId != null)
        {
            var value = dto.SmokingPreferenceId == -1 ? null : dto.SmokingPreferenceId;

            if (user.SmokingPreferenceId == value)
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            if (value != null &&
                !await _repo.ValidateSmokingPreference(value.Value))
                return (false, "Nieprawidłowa preferencja palenia.", null);

            user.SmokingPreferenceId = value;
            hasAnyChange = true;
        }

        if (dto.DrivingLicenseId != null)
        {
            var value = dto.DrivingLicenseId == -1 ? null : dto.DrivingLicenseId;

            if (user.DrivingLicenseId == value)
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            if (value != null &&
                !await _repo.ValidateDrivingLicense(value.Value))
                return (false, "Nieprawidłowe prawo jazdy.", null);

            user.DrivingLicenseId = value;
            hasAnyChange = true;
        }

        if (dto.TravelExperienceId != null)
        {
            var value = dto.TravelExperienceId == -1 ? null : dto.TravelExperienceId;

            if (user.TravelExperienceId == value)
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            if (value != null &&
                !await _repo.ValidateTravelExperience(value.Value))
                return (false, "Nieprawidłowe doświadczenie podróżnicze.", null);

            user.TravelExperienceId = value;
            hasAnyChange = true;
        }

        // =========================
        // OPTIONAL ENUMS M:M
        // =========================

        if (dto.LanguageIds != null)
        {
            var distinct = dto.LanguageIds.Distinct().ToList();

            if (distinct.Any() && !await _repo.ValidateLanguages(distinct))
                return (false, "Nieprawidłowe języki.", null);

            if (user.UserLanguages != null)
            {
                var current = user.UserLanguages.Select(x => x.IdLanguage).OrderBy(x => x).ToList();
                var incoming = distinct.OrderBy(x => x).ToList();

                if (current.SequenceEqual(incoming))
                    return (false, "Nie można zaktualizować danych na identyczne.", null);
            }

            user.UserLanguages?.Clear();

            foreach (var id in distinct)
            {
                user.UserLanguages!.Add(new UserLanguage
                {
                    IdUser = userId,
                    IdLanguage = id
                });
            }

            hasAnyChange = true;
        }

        if (dto.InterestIds != null)
        {
            var distinct = dto.InterestIds.Distinct().ToList();

            foreach (var id in distinct)
                if (!await _repo.ValidateInterest(id))
                    return (false, "Nieprawidłowe zainteresowania.", null);

            if (user.UserInterests != null)
            {
                var current = user.UserInterests.Select(x => x.IdInterest).OrderBy(x => x).ToList();
                var incoming = distinct.OrderBy(x => x).ToList();

                if (current.SequenceEqual(incoming))
                    return (false, "Nie można zaktualizować danych na identyczne.", null);
            }

            user.UserInterests?.Clear();

            foreach (var id in distinct)
            {
                user.UserInterests!.Add(new UserInterest
                {
                    IdUser = userId,
                    IdInterest = id
                });
            }

            hasAnyChange = true;
        }

        if (dto.TravelStyleIds != null)
        {
            var distinct = dto.TravelStyleIds.Distinct().ToList();

            foreach (var id in distinct)
                if (!await _repo.ValidateTravelStyle(id))
                    return (false, "Nieprawidłowe style podróży.", null);

            if (user.UserTravelStyles != null)
            {
                var current = user.UserTravelStyles.Select(x => x.IdTravelStyle).OrderBy(x => x).ToList();
                var incoming = distinct.OrderBy(x => x).ToList();

                if (current.SequenceEqual(incoming))
                    return (false, "Nie można zaktualizować danych na identyczne.", null);
            }

            user.UserTravelStyles?.Clear();

            foreach (var id in distinct)
            {
                user.UserTravelStyles!.Add(new UserTravelStyle
                {
                    IdUser = userId,
                    IdTravelStyle = id
                });
            }

            hasAnyChange = true;
        }

        if (dto.TransportModeIds != null)
        {
            var distinct = dto.TransportModeIds.Distinct().ToList();

            foreach (var id in distinct)
                if (!await _repo.ValidateTransportMode(id))
                    return (false, "Nieprawidłowe środki transportu.", null);

            if (user.UserTransportModes != null)
            {
                var current = user.UserTransportModes.Select(x => x.IdTransportMode).OrderBy(x => x).ToList();
                var incoming = distinct.OrderBy(x => x).ToList();

                if (current.SequenceEqual(incoming))
                    return (false, "Nie można zaktualizować danych na identyczne.", null);
            }

            user.UserTransportModes?.Clear();

            foreach (var id in distinct)
            {
                user.UserTransportModes!.Add(new UserTransportMode
                {
                    IdUser = userId,
                    IdTransportMode = id
                });
            }

            hasAnyChange = true;
        }

        // =========================
        // SAVE
        // =========================

        if (!hasAnyChange)
            return (false, "Nie można zaktualizować danych na identyczne.", null);

        user.UpdatedAt = DateTime.UtcNow;
        await _repo.SaveChangesAsync();

        return (true, null, new UserDTO
        {
            IdUser = user.IdUser,
            Username = user.Username,
            DisplayName = user.DisplayName,
            Email = user.Email,
            BirthDate = user.BirthDate,
            Gender = user.Gender?.GenderName,
            Pronouns = user.Pronouns?.PronounName,
            Location = user.Location,
            PersonalityType = user.PersonalityType?.PersonalityTypeName,
            AlcoholPreference = user.AlcoholPreference?.AlcoholPreferenceName,
            SmokingPreference = user.SmokingPreference?.SmokingPreferenceName,
            DrivingLicenseType = user.DrivingLicense?.DrivingLicenseName,
            TravelExperience = user.TravelExperience?.TravelExperienceName,
            Bio = user.Bio
        });
    }

    private string EnsureUniqueFileName(string directory, string fileName)
    {
        var name = Path.GetFileNameWithoutExtension(fileName);
        var ext = Path.GetExtension(fileName);

        var newFileName = fileName;
        var counter = 1;

        while (File.Exists(Path.Combine(directory, newFileName)))
        {
            newFileName = $"{name}_{counter}{ext}";
            counter++;
        }

        return newFileName;
    }

    private void DeleteFileIfExists(string? relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath)) return;

        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.TrimStart('/'));

        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    public async Task<(bool Success, string? Error, string? ProfilePath, string? BannerPath)> UpdateImagesAsync(
        int? userId, IFormFile? profileImage, IFormFile? bannerImage)
    {
        var user = await _repo.GetByIdUserAsync(userId);
        if (user == null)
            return (false, "User not found", null, null);

        var root = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "uploads",
            "users",
            userId.ToString()
        );

        var profileDir = Path.Combine(root, "profile");
        var bannerDir = Path.Combine(root, "banner");

        Directory.CreateDirectory(profileDir);
        Directory.CreateDirectory(bannerDir);

        string? profilePath = null;
        string? bannerPath = null;

        // =====================
        // PROFILE IMAGE
        // =====================
        if (profileImage != null)
        {
            if (profileImage.FileName == "-1")
            {
                DeleteFileIfExists(user.ProfilePhotoPath);

                user.ProfilePhotoPath = _placeholderProfilePath;
                profilePath = user.ProfilePhotoPath;
            }
            else
            {
                if (!profileImage.ContentType.StartsWith("image/"))
                    return (false, "Invalid profile image", null, null);

                DeleteFileIfExists(user.ProfilePhotoPath);

                var originalName = Path.GetFileName(profileImage.FileName);
                var safeName = $"{Guid.NewGuid()}_{originalName}";
                var finalName = EnsureUniqueFileName(profileDir, safeName);

                var fullPath = Path.Combine(profileDir, finalName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }

                profilePath = $"/uploads/users/{userId}/profile/{finalName}";
                user.ProfilePhotoPath = profilePath;
            }
        }

        // =====================
        // BANNER IMAGE
        // =====================
        if (bannerImage != null)
        {
            if (bannerImage.FileName == "-1")
            {
                DeleteFileIfExists(user.BackgroundPhotoPath);

                user.BackgroundPhotoPath = _placeholderBannerPath;
                bannerPath = user.BackgroundPhotoPath;
            }
            else
            {
                if (!bannerImage.ContentType.StartsWith("image/"))
                    return (false, "Invalid banner image", null, null);

                DeleteFileIfExists(user.BackgroundPhotoPath);

                var originalName = Path.GetFileName(bannerImage.FileName);
                var safeName = $"{Guid.NewGuid()}_{originalName}";
                var finalName = EnsureUniqueFileName(bannerDir, safeName);

                var fullPath = Path.Combine(bannerDir, finalName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await bannerImage.CopyToAsync(stream);
                }

                bannerPath = $"/uploads/users/{userId}/banner/{finalName}";
                user.BackgroundPhotoPath = bannerPath;
            }
        }

        user.UpdatedAt = DateTime.UtcNow;
        await _repo.SaveChangesAsync();

        return (true, null, profilePath, bannerPath);
    }

    public async Task<UserImagesDTO?> GetUserImagesAsync(int userId)
    {
        var user = await _repo.GetByIdUserAsync(userId);
        if (user == null) return null;

        return new UserImagesDTO
        {
            Profile = user.ProfilePhotoPath,
            Banner = user.BackgroundPhotoPath
        };
    }

    public async Task<(bool Success, string? Error)> UpdateCurrencyAsync(int? userId, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            return (false, "Nieprawidłowy kod waluty.");

        currency = currency.ToUpper();

        var available = await GetAvailableCurrenciesAsync();

        if (!available.Contains(currency))
            return (false, "Waluta nie jest wspierana.");

        var updated = await _repo.UpdateCurrencyAsync(userId, currency);

        if (!updated)
            return (false, "Użytkownik nie istnieje.");

        return (true, null);
    }

    public async Task<List<string>> GetAvailableCurrenciesAsync()
    {
        if (!File.Exists(_currencyFilePath))
            return new List<string>();

        var lines = await File.ReadAllLinesAsync(_currencyFilePath);

        return lines
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.Trim().ToUpper())
            .Distinct()
            .OrderBy(l => l)
            .ToList();
    }

    public async Task<List<UserSearchDTO>> SearchAsync(string query, int limit, int? currentUserId)
    {
        var users = await _repo.SearchAsync(query, limit);

        HashSet<int> blockedIds = new();

        if (currentUserId.HasValue)
        {
            blockedIds = await _blockedRepo.GetBlockedUserIdsAsync(currentUserId.Value);
        }

        return users
            .Where(u => !blockedIds.Contains(u.IdUser))
            .Select(u => new UserSearchDTO
            {
                Id = u.IdUser,
                Username = u.Username,
                DisplayName = u.DisplayName,
                ProfilePhotoPath = u.ProfilePhotoPath
            })
            .ToList();
    }

    public async Task<UserDTO?> GetByUsernameAsync(string username, int? currentUserId)
    {
        var u = await _repo.GetByUsernameAsync(username);
        if (u == null) return null;
        
        if (currentUserId != null)
        {
            var isBlocked = await _blockedRepo.ExistsEitherWay(currentUserId.Value, u.IdUser);

            if (isBlocked)
                return null;
        }

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

    public async Task<UserProfileDTO?> GetProfileByUsernameAsync(string username, int? currentUserId)
    {
        var user = await _repo.GetByUsernameAsync(username);
        if (user == null) return null;

        return await GetProfileByIdAsync(user.IdUser, currentUserId ?? 0);
    }
}