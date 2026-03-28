using backend.Language.DTOs;

namespace backend.Language.Services;

public interface ILanguageService
{
    Task<IEnumerable<LanguageDTO>> GetAllLanguagesAsync();
    Task<IEnumerable<LanguageDTO>> GetUserLanguagesAsync(int userId);
    Task<bool> UpdateUserLanguagesAsync(int userId, List<int> languageIds);
    Task SeedLanguagesAsync();
}