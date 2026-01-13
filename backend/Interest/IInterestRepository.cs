namespace backend.Interest;

public interface IInterestRepository
{
    Task<bool> AnyAsync();
    Task<bool> ExistsByNameAsync(string name);
    Task AddAsync(Models.Interest interest);
}