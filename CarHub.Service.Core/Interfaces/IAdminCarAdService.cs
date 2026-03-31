using CarHub.Data.Models;
using CarHub.ViewModels.CarAdAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarHub.Service.Core.Interfaces
{
    public interface IAdminCarAdService
    {
        Task<AdminCarAdPagedResultViewModel> GetAllAsync(string? searchTerm, int currentPage, int pageSize);
        Task<CarAd?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
