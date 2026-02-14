using CarHub.ViewModels.CarAdVMs;

namespace CarHub.Services.Interfaces
{
    public interface ICarAdService
    {
        Task<IEnumerable<CarAdIndexVM>> GetAllAsync();
        Task<CarAdDetailsVM?> GetDetailsAsync(int id);
        Task<bool> IsOwnerAsync(int carAdId, string userId);
        Task<CarAdCreateVM> GetCreateModelAsync();
        Task CreateAsync(CarAdCreateVM model, string ownerId);
        Task<IEnumerable<CategoryDropdownVM>> GetAllCategories();
        Task<CarAdCreateVM?> GetEditModelAsync(int id);
        Task UpdateAsync(CarAdCreateVM model,int id);
        Task<CarAdDeleteVM?> GetDeleteModelAsync(int id);
        Task DeleteAsync(int id);
        Task<IEnumerable<CarAdIndexVM>> GetMineAsync(string userId);
    }
}
