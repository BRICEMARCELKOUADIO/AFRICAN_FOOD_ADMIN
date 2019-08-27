using AFRICAN_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFRICAN_FOOD.Contracts.Services.Data
{
    public interface ICatalogDataService
    {
        Task<IEnumerable<Pie>> GetAllPiesAsync();
        Task<IEnumerable<Pie>> GetPiesOfTheWeekAsync();
        //Task<Pie> AddPiesAsync(
        //    string name, string shortDescription, decimal price,decimal prixPromotionnel, 
        //    byte[] image,  bool isPieOfTheWeek, bool inStock, string userAdminId);
        Task<Pie> AddPiesAsync(
            string name, string shortDescription, decimal price, decimal prixPromotionnel,
            string imageToBase64, bool isPieOfTheWeek, bool inStock, string userAdminId);

        Task<List<Pie>> GetPieByAdminId(string id);
    }
}
