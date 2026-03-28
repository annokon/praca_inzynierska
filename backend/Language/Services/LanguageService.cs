using backend.Language.DTOs;
using backend.Language.Repositories;

namespace backend.Language.Services;

public class LanguageService : ILanguageService
{
    private readonly ILanguageRepository _languageRepository;

    public LanguageService(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    public async Task<IEnumerable<LanguageDTO>> GetAllLanguagesAsync()
    {
        var langs = await _languageRepository.GetAllLanguagesAsync();

        var all = langs.Select(l => new LanguageDTO
        {
            Id = l.IdLanguage,
            Name = l.LanguageName
        }).ToList();

        var popular = new List<string>
        {
            "polski",
            "angielski",
            "ukraiński",
            "włoski",
            "niemiecki",
            "francuski"
        };

        var popularFirst = all
            .Where(l => popular.Contains(l.Name, StringComparer.OrdinalIgnoreCase))
            .OrderBy(l => popular.IndexOf(l.Name.ToLower()))
            .ToList();

        var others = all
            .Where(l => !popular.Contains(l.Name, StringComparer.OrdinalIgnoreCase))
            .OrderBy(l => l.Name)
            .ToList();

        return popularFirst.Concat(others);
    }

    public async Task<IEnumerable<LanguageDTO>> GetUserLanguagesAsync(int userId)
    {
        var langs = await _languageRepository.GetUserLanguagesAsync(userId);

        return langs.Select(l => new LanguageDTO
        {
            Id = l.IdLanguage,
            Name = l.LanguageName
        });
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

        foreach (var code in pl.Keys)
        {
            var lang = new Language
            {
                LanguageName = pl[code]
            };

            await _languageRepository.AddLanguageAsync(lang);
        }
    }
}