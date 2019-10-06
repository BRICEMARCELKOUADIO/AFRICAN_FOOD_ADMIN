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
        private bool _canGo = false;

        public LoginViewModel(IConnectionService connectionService, ISettingsService settingsService,
            INavigationService navigationService,
            IAuthenticationService authenticationService,
            IDialogService dialogService)
            : base(connectionService, navigationService, dialogService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }

        public ICommand LoginCommand => new Command(OnLogin, () => CanGo);
        public ICommand RegisterCommand => new Command(OnRegister);

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                CanExecute();
            }
        }

        public bool CanGo
        {
            get
            {
                return _canGo;
            }
            set
            {
                _canGo = value;
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
                CanExecute();
            }
        }

        private async void OnLogin()
        {
            IsBusy = true;
            if (_connectionService.IsConnected)
            {
                
                var authenticationResponse = await _authenticationService.Authenticate(Email, Password);

                if (authenticationResponse == null)
                {
                    await _dialogService.ShowDialog(
                            "Impossible de se connecter au serveur",
                            "Erreur",
                            "OK");
                    IsBusy = true;
                    return;
                }

                if (authenticationResponse.IsAuthenticated)
                {
                    if (authenticationResponse.User.TypeUser == false)
                    {
                        await _dialogService.ShowDialog(
                            "Vous n'êtes pas autorisé a vous connecter a cette Application",
                            "Erreur lors de la connexion",
                            "OK");
                        IsBusy = false;
                        return;
                    }

                    // we store the Id to know if the user is already logged in to the application
                    _settingsService.UserIdSetting =  authenticationResponse.User.Id;
                    _settingsService.UserPhone = authenticationResponse.User.UserPhone;
                    _settingsService.UserNameSetting = authenticationResponse.User.FirstName;
                    _settingsService.Latitude = authenticationResponse.User.Latitude;
                    _settingsService.Longitude = authenticationResponse.User.Longitude;
                    _settingsService.Position = authenticationResponse.User.PositionGeo;

                    IsBusy = false;
                    await _navigationService.NavigateToAsync<MainViewModel>();
                }
                else
                {
                    await _dialogService.ShowDialog(
                        "Cette combinaison nom d'utilisateur / mot de passe n'est pas connue",
                        "Erreur lors de la connexion",
                        "OK");
                }
            }
            else
            {
                await _dialogService.ShowDialog(
                    "Cette combinaison nom d'utilisateur / mot de passe n'est pas connue",
                    "Erreur lors de la connexion",
                    "OK");
            }
            IsBusy = false;
        }

        private void CanExecute()
        {
            CanGo = !(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password));
        }

        private void OnRegister()
        {
            _navigationService.NavigateToAsync<RegistrationViewModel>();
        }
    }
}
