namespace backend.Language.Repositories;

public interface ILanguageRepository
{
    Task<IEnumerable<Language>> GetAllLanguagesAsync();
    Task<IEnumerable<Language>> GetUserLanguagesAsync(int userId);
    Task<bool> UpdateUserLanguagesAsync(int userId, List<int> languageIds);
    Task AddLanguageAsync(Language language);
    Task<Dictionary<string, string>> LoadLanguagesAsync(string langCode);
}