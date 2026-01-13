namespace backend.Interest;

public interface IInterestRepository
{
    Task<IEnumerable<Models.Interest>> GetAllAsync();
    Task<bool> AnyAsync();
    Task<bool> ExistsByNameAsync(string name);
    Task AddAsync(Models.Interest interest);
}