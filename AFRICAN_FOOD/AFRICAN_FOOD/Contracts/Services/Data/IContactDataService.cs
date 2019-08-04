using AFRICAN_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFRICAN_FOOD.Contracts.Services.Data
{
    public interface IContactDataService
    {
        Task<ContactInfo> AddContactInfo(ContactInfo contactInfo);
    }
}
