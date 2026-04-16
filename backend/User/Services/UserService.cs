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

    // login
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

    // get user by id
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

    // get user
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
        };
    }

    // update user
    public async Task<(bool Success, string? Error, UserDTO? User)> UpdateProfileAsync(int userId, UpdateUserProfileDTO dto)
    {
        var user = await userRepository.GetUserWithRelationsAsync(userId);
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

            if (await userRepository.ExistsByUsernameAsync(username))
                return (false, "Ta nazwa użytkownika jest już zajęta.", null);

            user.Username = username;
            hasAnyChange = true;
        }

        if (dto.Email != null)
        {
            var email = dto.Email.Trim().ToLowerInvariant();

            try { _ = new MailAddress(email); }
            catch { return (false, "Nieprawidłowy e-mail.", null); }

            if (string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase))
                return (false, "Nie można zaktualizować danych na identyczne.", null);

            if (await userRepository.ExistsByEmailAsync(email))
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
                !await userRepository.ValidateGender(value.Value))
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
                !await userRepository.ValidatePronouns(value.Value))
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
                !await userRepository.ValidatePersonalityType(value.Value))
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
                !await userRepository.ValidateAlcoholPreference(value.Value))
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
                !await userRepository.ValidateSmokingPreference(value.Value))
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
                !await userRepository.ValidateDrivingLicense(value.Value))
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
                !await userRepository.ValidateTravelExperience(value.Value))
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

            if (distinct.Any() && !await userRepository.ValidateLanguages(distinct))
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
                if (!await userRepository.ValidateInterest(id))
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
                if (!await userRepository.ValidateTravelStyle(id))
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
                if (!await userRepository.ValidateTransportMode(id))
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
        await userRepository.SaveChangesAsync();

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
}