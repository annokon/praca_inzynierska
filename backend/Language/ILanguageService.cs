namespace backend.Language;

public interface ILanguageService
{
    Task<IEnumerable<Models.Language>> GetAllLanguagesAsync();
    Task<IEnumerable<Models.Language>> GetUserLanguagesAsync(int userId);
    Task<bool> UpdateUserLanguagesAsync(int userId, List<int> languageIds);
    Task SeedLanguagesAsync();
}