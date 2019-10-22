using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.ViewModels
{
    public class UserProfilViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingsService _settingsService;

        //private string _firstName;
        //private string _lastName;
        //private string _password;
        //private string _email;
        //private string _userPhone;
        //private string _commerceName;
        //private string _commerceLocate;
        //private bool _canGo = false;
        //private string _position = string.Empty;
        //private double _longitude;
        //private double _latitude;
        public UserProfilViewModel(IConnectionService connectionService, INavigationService navigationService, IAuthenticationService authenticationService, ISettingsService settingsService, IDialogService dialogService) : base(connectionService, navigationService, dialogService)
        {
            _authenticationService = authenticationService;
            _settingsService = settingsService;
        }
    }
}
