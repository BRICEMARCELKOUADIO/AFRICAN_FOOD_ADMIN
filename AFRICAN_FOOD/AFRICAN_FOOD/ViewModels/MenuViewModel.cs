using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.Enumerations;
using AFRICAN_FOOD.Models;
using AFRICAN_FOOD.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AFRICAN_FOOD.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private ObservableCollection<MainMenuItem> _menuItems;
        private readonly ISettingsService _settingsService;

        public MenuViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            ISettingsService settingsService)
            : base(connectionService, navigationService, dialogService)
        {
            _settingsService = settingsService;
            MenuItems = new ObservableCollection<MainMenuItem>();
            LoadMenuItems();
        }

        public string WelcomeText => "Bonjour " + _settingsService.UserNameSetting;

        public ICommand MenuItemTappedCommand => new Command(OnMenuItemTapped);

        public ObservableCollection<MainMenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                OnPropertyChanged();
            }
        }

        private void OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            var menuItem = ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as MainMenuItem);

            if (menuItem != null && menuItem.MenuText == "Deconnexion")
            {
                _settingsService.UserIdSetting = null;
                _settingsService.UserNameSetting = null;
                _navigationService.ClearBackStack();
            }

            var type = menuItem?.ViewModelToLoad;
            _navigationService.NavigateToAsync(type);
        }

        private void LoadMenuItems()
        {
            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Accueil",
                ViewModelToLoad = typeof(MainViewModel),
                MenuItemType = MenuItemType.Home
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Ajouter un produit",
                ViewModelToLoad = typeof(PieCatalogViewModel),
               MenuItemType = MenuItemType.Pies
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Commandes en cours",
                ViewModelToLoad = typeof(ShoppingCartViewModel),
                MenuItemType = MenuItemType.ShoppingCart
            });

            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Info Utilisateur",
                ViewModelToLoad = typeof(UserProfilViewModel),
                MenuItemType = MenuItemType.profil
            });

            //MenuItems.Add(new MainMenuItem
            //{
            //    MenuText = "Contact us",
            //    ViewModelToLoad = typeof(ContactViewModel),
            //    MenuItemType = MenuItemType.Contact
            //});



            MenuItems.Add(new MainMenuItem
            {
                MenuText = "Déconnexion",
                ViewModelToLoad = typeof(LoginViewModel),
                MenuItemType = MenuItemType.Logout
            });
        }
    }
}
