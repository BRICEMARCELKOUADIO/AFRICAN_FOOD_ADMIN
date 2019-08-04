using AFRICAN_FOOD.Constants;
using AFRICAN_FOOD.Contracts.Repository;
using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Models;
using Akavache;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFRICAN_FOOD.Services.Data
{
    public class ContactDataService : BaseService, IContactDataService
    {
        private readonly IGenericRepository _genericRepository;

        public ContactDataService(IGenericRepository genericRepository, IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<ContactInfo> AddContactInfo(ContactInfo contactInfo)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AddContactInfoEndpoint
            };

            var result = await _genericRepository.PostAsync(builder.ToString(), contactInfo);

            return result;
        }
    }
}
