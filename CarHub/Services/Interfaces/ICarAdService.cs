using CarHub.ViewModels.CarAdVMs;

namespace CarHub.Services.Interfaces
{
    public interface ICarAdService
    {
        Task<IEnumerable<CarAdIndexVM>> GetAllAsync();

        Task<CarAdDetailsVM?> GetDetailsAsync(int id);

        Task<bool> IsOwnerAsync(int carAdId, string userId);
    }
}
