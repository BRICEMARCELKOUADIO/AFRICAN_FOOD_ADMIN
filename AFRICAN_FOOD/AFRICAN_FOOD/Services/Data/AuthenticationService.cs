using AFRICAN_FOOD.Constants;
using AFRICAN_FOOD.Contracts.Repository;
using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.Models;
using System;
using System.Collections.Generic;
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

        public async Task<AuthenticationResponse> Register(string firstName, string lastName, string email, bool typeuser, string userName, string password)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.RegisterEndpoint
            };

            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserPhone = userName,
                Password = password,
                TypeUser = typeuser

            };

            return await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(builder.ToString(), authenticationRequest);
        }

        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(_settingsService.UserIdSetting);
        }

        public async Task<AuthenticationResponse> Authenticate(string Email, string password)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AuthenticateEndpoint
            };

            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                Email = Email,
                Password = password
            };

            return await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(builder.ToString(), authenticationRequest);
        }
    }
}
