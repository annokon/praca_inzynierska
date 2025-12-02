namespace backend.Language;

public class LanguageService : ILanguageService
{
    private readonly ILanguageRepository _languageRepository;

    public LanguageService(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    public async Task<IEnumerable<Models.Language>> GetAllLanguagesAsync()
    {
        return await _languageRepository.GetAllLanguagesAsync();
    }

    public async Task<IEnumerable<Models.Language>> GetUserLanguagesAsync(int userId)
    {
        return await _languageRepository.GetUserLanguagesAsync(userId);
    }

    public async Task<bool> UpdateUserLanguagesAsync(int userId, List<int> languageIds)
    {
        return await _languageRepository.UpdateUserLanguagesAsync(userId, languageIds);
    }

    public async Task SeedLanguagesAsync()
    {
        var existing = await _languageRepository.GetAllLanguagesAsync();
        if (existing.Any())
            return;

        var pl = await _languageRepository.LoadLanguagesAsync("pl");
        var en = await _languageRepository.LoadLanguagesAsync("en");

        foreach (var code in pl.Keys)
        {
            if (!pl.ContainsKey(code) || !en.ContainsKey(code))
                continue;

            var lang = new Models.Language
            {
                LanguageNamePL = pl[code],
                LanguageNameEN = en[code]
            };

            await _languageRepository.AddLanguageAsync(lang);
        }
    }
}