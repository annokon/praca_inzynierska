namespace backend.Interest.Repositories;

public interface IInterestRepository
{
    Task<IEnumerable<Interest>> GetAllAsync();
    Task<bool> AnyAsync();
    Task<bool> ExistsByNameAsync(string name);
    Task AddAsync(Interest interest);
}