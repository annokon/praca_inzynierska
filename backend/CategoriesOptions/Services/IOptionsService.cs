namespace backend.CategoriesOptions.Services;

public interface IOptionsService
{
    Task<object> GetAllOptionsAsync();
}