using CarHub.ViewModels.CarAdVMs;

namespace CarHub.Services.Interfaces
{
    public interface IFavouriteService
    {
        Task AddAsync(int carAdId, string userId);
        Task RemoveAsync(int carAdId, string userId);
        Task<bool> IsFavouriteAsync(int carAdId, string userId);
        Task<IEnumerable<CarAdIndexVM>> GetMyFavouritesAsync(string userId);
    }
}
