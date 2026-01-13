using backend.Interest.DTOs;

namespace backend.Interest;

public class InterestService : IInterestService
{
    private readonly IInterestRepository _repo;
    private readonly IWebHostEnvironment _env;

    public InterestService(IInterestRepository repo, IWebHostEnvironment env)
    {
        _repo = repo;
        _env = env;
    }

    public async Task SeedInterestsAsync()
    {
        if (await _repo.AnyAsync())
        {
            Console.WriteLine("Interests already exist. Seed skipped.");
            return;
        }

        var filePath = Path.Combine(_env.ContentRootPath, 
            "Resources", "Interests", "interests.pl.txt");

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Interest seed file not found!");
            return;
        }

        var lines = await File.ReadAllLinesAsync(filePath);

        foreach (var line in lines)
        {
            var name = line.Trim();
            if (string.IsNullOrWhiteSpace(name))
                continue;

            if (await _repo.ExistsByNameAsync(name))
                continue;

            await _repo.AddAsync(new Models.Interest
            {
                InterestName = name
            });
        }

        Console.WriteLine("Interests imported from intrests.pl.txt");
    }
    
    public async Task<IEnumerable<InterestDTO>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();

        return items.Select(i => new InterestDTO
        {
            Id = i.IdInterest,
            Name = i.InterestName
        });
    }
}
