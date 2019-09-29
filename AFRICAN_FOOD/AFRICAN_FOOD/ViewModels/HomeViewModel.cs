using AFRICAN_FOOD.Constants;
using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.Extensions;
using AFRICAN_FOOD.Models;
using AFRICAN_FOOD.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AFRICAN_FOOD.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ICatalogDataService _catalogDataService;
        public readonly ISettingsService _settingsService;
        private ObservableCollection<Pie> _piesOfTheWeek;

        public HomeViewModel(IConnectionService connectionService,
            INavigationService navigationService,
            IDialogService dialogService,
            ICatalogDataService catalogDataService,
            ISettingsService settingsService) : base(connectionService, navigationService, dialogService)
        {
            _catalogDataService = catalogDataService;
            _settingsService = settingsService;

            PiesOfTheWeek = new ObservableCollection<Pie>();
            MessagingCenter.Subscribe<PieCatalogViewModel>(this, "Pies_added", refreshpie);
            //initialise();
        }

        private async void refreshpie(PieCatalogViewModel obj)
        {
            string id = _settingsService.UserIdSetting;
            PiesOfTheWeek.Clear();
            PiesOfTheWeek = (await _catalogDataService.GetPieByAdminId(id)).ToObservableCollection();
        }

        public ICommand PieTappedCommand => new Command<Pie>(OnPieTapped);
        public ICommand AddToCartCommand => new Command<Pie>(OnAddToCart);
        public ICommand EditCommand => new Command<Pie>(OnEdit);
        public ICommand DeletCommand => new Command<Pie>(OnDelet);

        public ObservableCollection<Pie> PiesOfTheWeek
        {
            get => _piesOfTheWeek;
            set
            {
                _piesOfTheWeek = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadProduct = false;
        public bool IsLoadProduct
        {
            get => _isLoadProduct;
            set
            {
                _isLoadProduct = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            IsLoadProduct = true;
            string id = _settingsService.UserIdSetting;
            PiesOfTheWeek = (await _catalogDataService.GetPieByAdminId(id)).ToObservableCollection();
            IsLoadProduct = false;
        }

        private void OnPieTapped(Pie selectedPie)
        {
            _navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
        }

        private async void OnAddToCart(Pie selectedPie)
        {
            IsBusy = true;
            MessagingCenter.Send(this, MessagingConstants.AddPieToBasket, selectedPie);
            await _dialogService.ShowDialog("Produit ajoutée à votre panier", "Succès", "OK");
            PiesOfTheWeek = (await _catalogDataService.GetPiesOfTheWeekAsync()).ToObservableCollection();
            IsBusy = false;
        }

        private async void OnDelet(Pie selectedPie)
        {
            IsBusy = true;
            var response = await _catalogDataService.DeletPie(selectedPie.PieId);
            if (response != null)
                await _dialogService.ShowDialog("Votre produit a été supprimer", "", "Ok");
            else
                await _dialogService.ShowDialog("Error lors de suppression de votre produit", "", "Ok");

            PieRefresh();

            IsBusy = false;
        }

        private async void PieRefresh()
        {
            IsLoadProduct = true;
            PiesOfTheWeek.Clear();
            string id = _settingsService.UserIdSetting;
            PiesOfTheWeek = (await _catalogDataService.GetPieByAdminId(id)).ToObservableCollection();
            IsLoadProduct = false;
        }

        private async void OnEdit(Pie selectedPie)
        {
            await _navigationService.NavigateToAsync<PieCatalogViewModel>(selectedPie);
        }
    }
}
