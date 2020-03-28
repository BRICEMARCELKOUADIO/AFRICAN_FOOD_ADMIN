using AFRICAN_FOOD.Constants;
using AFRICAN_FOOD.Contracts.Repository;
using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFRICAN_FOOD.Services.Data
{
    public class TchatDataService : ITchatDataService
    {
        private readonly IGenericRepository _genericRepository;

        public TchatDataService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<IEnumerable<User>> GetAllUser()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.GetAllUser
            };
            try
            {
                var users = await _genericRepository.GetAsync<List<User>>(builder.ToString());
                return users;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return new List<User>();
        }

        public async Task<Message> GetMessages(string Userid, string idRecever)
        {

            //UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            //{
            //    Path = ApiConstants.GetAllMessage + "?Userid=" + Userid + "&" + "idRecever=" + idRecever
            //};

            var url = $"{ApiConstants.BaseApiUrl}{ApiConstants.GetAllMessage}?Userid={Userid}&idRecever={idRecever}";


            var messages = await _genericRepository.GetAsync<Message>(url);

            return messages;
        }

        public async Task<MessageDetail> SendMessage(MessageDetail messageDetail)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.SendMessage
            };

            var result = await _genericRepository.PostAsync<MessageDetail, MessageDetail>(builder.ToString(), messageDetail);

            return result;
        }
    }
}
