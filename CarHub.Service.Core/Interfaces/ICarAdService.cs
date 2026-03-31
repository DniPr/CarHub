using CarHub.ViewModels.CarAdVMs;

namespace CarHub.Service.Core.Interfaces
{
    public interface ICarAdService
    {
        Task<CarAdPagedResultViewModel> GetAllAsync(string? searchTerm, int currentPage, int pageSize);
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
