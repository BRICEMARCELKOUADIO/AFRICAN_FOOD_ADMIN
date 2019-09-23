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
        Task<Pie> AddPiesAsync(int pieId, string name, string shortDescription, decimal price, decimal prixPromotionnel, string imageToBase64, /*byte[] image,*/ bool isPieOfTheWeek, bool inStock, string userAdminId, string userPhone, double longitude, double latitude, string positionGeo, bool isModify);

        Task<List<Pie>> GetPieByAdminId(string id);
        Task<Pie> DeletPie(int pieId);
    }
}
