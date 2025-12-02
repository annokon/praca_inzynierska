using System.Text.Json;
using backend.Infrastructure.Data;
using backend.Language.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Language;

public class LanguageRepository : ILanguageRepository
{
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _env;

    public LanguageRepository(DataContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IEnumerable<Models.Language>> GetAllLanguagesAsync()
    {
        return await _context.Languages
            .OrderBy(l => l.LanguageNamePL)
            .ToListAsync();
    }

    public async Task<IEnumerable<Models.Language>> GetUserLanguagesAsync(int userId)
    {
        return await _context.UserLanguages
            .Where(ul => ul.IdUser == userId)
            .Include(ul => ul.Language)
            .Select(ul => ul.Language)
            .ToListAsync();
    }

    public async Task<bool> UpdateUserLanguagesAsync(int userId, List<int> languageIds)
    {
        var userExists = await _context.Users.AnyAsync(u => u.IdUser == userId);
        if (!userExists) return false;

        var old = _context.UserLanguages.Where(ul => ul.IdUser == userId);
        _context.UserLanguages.RemoveRange(old);

        foreach (var idLang in languageIds)
        {
            _context.UserLanguages.Add(new UserLanguage
            {
                IdUser = userId,
                IdLanguage = idLang
            });
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task AddLanguageAsync(Models.Language language)
    {
        await _context.Languages.AddAsync(language);
        await _context.SaveChangesAsync();
    }

    public async Task<Dictionary<string, string>> LoadLanguagesAsync(string langCode)
    {
        var path = Path.Combine(
            _env.ContentRootPath,
            "Resources",
            "Languages",
            $"languages.{langCode}.json"
        );

        if (!File.Exists(path))
            throw new FileNotFoundException($"CLDR file missing: {path}");

        var json = await File.ReadAllTextAsync(path);

        var root = JsonSerializer.Deserialize<CldrLanguagesRootDTO>(json);

        return root!.main[langCode].localeDisplayNames.languages;
    }
}