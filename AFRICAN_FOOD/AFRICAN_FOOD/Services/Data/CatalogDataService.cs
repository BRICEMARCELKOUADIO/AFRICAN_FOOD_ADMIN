using AFRICAN_FOOD.Constants;
using AFRICAN_FOOD.Contracts.Repository;
using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Models;
using Akavache;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFRICAN_FOOD.Services.Data
{
    public class CatalogDataService : BaseService, ICatalogDataService
    {
        private readonly IGenericRepository _genericRepository;

        public CatalogDataService(IGenericRepository genericRepository,
            IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Pie>> GetAllPiesAsync()
        {
            List<Pie> piesFromCache =
                await GetFromCache<List<Pie>>(CacheNameConstants.AllPies);

            if (piesFromCache != null)//loaded from cache
            {
                return piesFromCache;
            }
            else
            {
                UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
                {
                    Path = ApiConstants.CatalogEndpoint
                };

                var pies = await _genericRepository.GetAsync<List<Pie>>(builder.ToString());

                await Cache.InsertObject(CacheNameConstants.AllPies, pies, DateTimeOffset.Now.AddSeconds(20));

                return pies;
            }
        }

        public async Task<IEnumerable<Pie>> GetPiesOfTheWeekAsync()
        {
            //List<Pie> piesFromCache = await GetFromCache<List<Pie>>(CacheNameConstants.PiesOfTheWeek);

            //if (piesFromCache != null)//loaded from cache
            //{
            //    return piesFromCache;
            //}

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.PiesOfTheWeekEndpoint
            };

            var pies = await _genericRepository.GetAsync<List<Pie>>(builder.ToString());

           // await Cache.InsertObject(CacheNameConstants.PiesOfTheWeek, pies, DateTimeOffset.Now.AddSeconds(20));

            return pies;
        }

        public async Task<Pie> AddPiesAsync(string name, string shortDescription,  decimal price, decimal prixPromotionnel, string imageToBase64, /*byte[] image,*/ bool isPieOfTheWeek, bool inStock, string userAdminId)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.PiesOfTheWeekEndpoint
            };
            var pies = new Pie();
            var url = $"{ApiConstants.BaseApiUrl}{ApiConstants.AddPies}?Name={name}&ShortDescription={shortDescription}&Price={price}&PrixPromotionnel={prixPromotionnel}&IsPieOfTheWeek={isPieOfTheWeek}&inStock={inStock}&UserAdminId={userAdminId}&Image={imageToBase64}";

            var objectToSend = new Pie
            {
                Name = name,
                Price = price,
                ShortDescription = shortDescription,
                Image = imageToBase64,
                IsPieOfTheWeek = isPieOfTheWeek,
                InStock = inStock,
                UserAdminId = userAdminId,
                PrixPromotionnel = prixPromotionnel
            };

            //var url = $"{ApiConstants.BaseApiUrl}{ApiConstants.AddPies}?Name={name}&ShortDescription={shortDescription}&Price={price}&PrixPromotionnel={prixPromotionnel}&IsPieOfTheWeek={isPieOfTheWeek}&inStock={inStock}&UserAdminId={userAdminId}";
            var result = await _genericRepository.PostAsync<Pie,Pie>($"{ApiConstants.BaseApiUrl}{ApiConstants.AddPies}", objectToSend);
            //PostAsync<T, TR>(string uri, T data, string authToken = "")

            return result;
        }

        public async Task<List<Pie>> GetPieByAdminId(string id)
        {
            //List<Pie> piesFromCache =
            //    await GetFromCache<List<Pie>>(CacheNameConstants.AllPies);

            //if (piesFromCache != null)//loaded from cache
            //{
            //    return piesFromCache;
            //}
            //else
            //{
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.GetPieByAdminId + id
            };

                var pies = await _genericRepository.GetAsync<List<Pie>>(builder.ToString());

               // await Cache.InsertObject(CacheNameConstants.AllPies, pies, DateTimeOffset.Now.AddSeconds(20));

                return pies;
            //}
        }


    }
}
