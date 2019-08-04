using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AFRICAN_FOOD.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        private string _email;
        private string _password;

        public LoginViewModel(IConnectionService connectionService, ISettingsService settingsService,
            INavigationService navigationService,
            IAuthenticationService authenticationService,
            IDialogService dialogService)
            : base(connectionService, navigationService, dialogService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }

        public ICommand LoginCommand => new Command(OnLogin);
        public ICommand RegisterCommand => new Command(OnRegister);

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private async void OnLogin()
        {
            IsBusy = true;
            if (_connectionService.IsConnected)
            {
                var authenticationResponse = await _authenticationService.Authenticate(Email, Password);

                if (authenticationResponse.IsAuthenticated)
                {
                    // we store the Id to know if the user is already logged in to the application
                    _settingsService.UserIdSetting = authenticationResponse.User.Id;
                    _settingsService.UserNameSetting = authenticationResponse.User.FirstName;

                    IsBusy = false;
                    await _navigationService.NavigateToAsync<MainViewModel>();
                }
            }
            else
            {
                await _dialogService.ShowDialog(
                    "This username/password combination isn't known",
                    "Error logging you in",
                    "OK");
            }
        }

        private void OnRegister()
        {
            _navigationService.NavigateToAsync<RegistrationViewModel>();
        }
    }
}
