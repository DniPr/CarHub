using CarHub.ViewModels.CarAdVMs;

namespace CarHub.Services.Interfaces
{
    public interface IFavouriteService
    {
        Task AddAsync(int carAdId, string userId);
        Task RemoveAsync(int carAdId, string userId);
        Task<bool> IsFavoriteAsync(int carAdId, string userId);
        Task<IEnumerable<CarAdIndexVM>> GetMyFavoritesAsync(string userId);
    }
}
