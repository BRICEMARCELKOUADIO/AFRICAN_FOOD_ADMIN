using AFRICAN_FOOD.Constants;
using AFRICAN_FOOD.Contracts.Repository;
using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AFRICAN_FOOD.Services.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;
        public AuthenticationService(IGenericRepository genericRepository, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _genericRepository = genericRepository;

        }

        public async Task<AuthenticationResponse> Register(string firstName, string lastName, string email, bool typeuser,string commerceName,string commerceLocate, string usePhone, string password, string longitude, string latitude, string position,bool Ismodify, string oldEmail)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.RegisterEndpoint
            };


            var url = $"{ApiConstants.BaseApiUrl}{ ApiConstants.RegisterEndpoint}?FirstName={firstName}&LastName={lastName}&Email={email}&TypeUser={typeuser}&CommerceName={commerceName}&CommerceLocate={commerceLocate}&UserPhone={usePhone}&Longitude={longitude}&Latitude={latitude}&PositionGeo={position}&Password={password}&Ismodify={Ismodify}&oldEmail={oldEmail}";

            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserPhone = usePhone,
                password = password,
                TypeUser = typeuser

            };

            return await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(url, authenticationRequest);
        }

        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(_settingsService.UserIdSetting);
        }

        public async Task<AuthenticationResponse> Authenticate(string Email, string password)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AuthenticateEndpoint //$"{ ApiConstants.AuthenticateEndpoint}?Email={Email}&password={password}"

            };

            var url = $"{ApiConstants.BaseApiUrl}{ ApiConstants.AuthenticateEndpoint}?Email={Email}&password={password}";
            //--
            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                Email = Email,
                password = password
            };

            
           // var content = new FormUrlEncodedContent(authenticationRequest);
            //return await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(builder.ToString(), authenticationRequest);
            return await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(url, authenticationRequest);
            //return await _genericRepository.PostAsync<AuthenticationResponse>(builder.ToString());
        }

        public async Task<AuthenticationResponse> DeleteMyCompte(string id)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.DeleteUser + $"/{id}"
            };

            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                Email = ""
            };

            var url = $"{ApiConstants.BaseApiUrl}{ ApiConstants.DeleteUser}?id={id}";

            return await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(url, authenticationRequest);
        }
    }
}
