namespace backend.Language.DTOs;

public class CldrLanguagesRootDTO
{
    public Dictionary<string, CldrLocaleDTO> main { get; set; } = new();
}