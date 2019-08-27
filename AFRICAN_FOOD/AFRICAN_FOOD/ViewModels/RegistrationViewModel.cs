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
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        private string _firstName;
        private string _lastName;
        private string _password;
        private string _email;
        private string _userPhone;
        private string _commerceName;
        private string _commerceLocate;

        public RegistrationViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            IAuthenticationService authenticationService, ISettingsService settingsService)
            : base(connectionService, navigationService, dialogService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string CommerceName
        {
            get => _commerceName;
            set
            {
                _commerceName = value;
                OnPropertyChanged();
            }
        }

        public string CommerceLocate
        {
            get => _commerceLocate;
            set
            {
                _commerceLocate = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string UserPhone
        {
            get => _userPhone;
            set
            {
                _userPhone = value;
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

        public ICommand RegisterCommand => new Command(OnRegister);
        public ICommand LoginCommand => new Command(OnLogin);

        private async void OnRegister()
        {
            if (_connectionService.IsConnected)
            {

                var userRegistered = await
                    _authenticationService.Register(_firstName, _lastName, _email, true, _commerceName, _commerceLocate, _userPhone, _password); ;

                if (userRegistered.IsAuthenticated)
                {
                    await _dialogService.ShowDialog("inscription réussi", "", "OK");
                    _settingsService.UserIdSetting = userRegistered.User.Id;
                    await _navigationService.NavigateToAsync<LoginViewModel>();
                }
                else
                {
                    await _dialogService.ShowDialog("Ce utilisateur existe déjà", "", "OK");
                    return;
                }

            }

            else
            {
                await _dialogService.ShowDialog("Vérifier votre connexion internet", "", "OK");
            }
        }

        private void OnLogin()
        {
            _navigationService.NavigateToAsync<LoginViewModel>();
        }
    }
}
