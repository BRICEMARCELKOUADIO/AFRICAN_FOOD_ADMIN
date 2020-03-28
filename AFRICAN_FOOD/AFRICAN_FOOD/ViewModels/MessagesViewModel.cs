using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.Extensions;
using AFRICAN_FOOD.Models;
using AFRICAN_FOOD.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AFRICAN_FOOD.ViewModels
{
    public class MessagesViewModel : ViewModelBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IDialogService _dialogService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ITchatDataService _tchatDataService;

        private ObservableCollection<User> _userTchat = new ObservableCollection<User>();
        public ObservableCollection<User> UserTchat
        {
            get => _userTchat;
            set
            {
                _userTchat = value;
                OnPropertyChanged();
            }
        }

        public ICommand UserTappedCommand => new Command<User>(OnUserTapped);

        public MessagesViewModel(IConnectionService connectionService, INavigationService navigationService, IDialogService dialogService, ITchatDataService tchatDataService) : base(connectionService, navigationService, dialogService)
        {
            _tchatDataService = tchatDataService;
        }

        private void OnUserTapped(User obj)
        {
            _navigationService.NavigateToAsync<MessageViewModel>(obj);
        }

        public override async Task InitializeAsync(object data)
        {
            IsBusy = true;
            UserTchat = (await _tchatDataService.GetAllUser()).ToObservableCollection();
            IsBusy = false;
        }
    }
}
