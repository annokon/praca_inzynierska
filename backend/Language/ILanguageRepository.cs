namespace backend.Language;

public interface ILanguageRepository
{
    Task<IEnumerable<Models.Language>> GetAllLanguagesAsync();
    Task<IEnumerable<Models.Language>> GetUserLanguagesAsync(int userId);
    Task<bool> UpdateUserLanguagesAsync(int userId, List<int> languageIds);
    Task AddLanguageAsync(Models.Language language);
    Task<Dictionary<string, string>> LoadLanguagesAsync(string langCode);
}