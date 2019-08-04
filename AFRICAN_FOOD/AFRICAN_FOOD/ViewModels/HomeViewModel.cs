using AFRICAN_FOOD.Constants;
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
    public class HomeViewModel : ViewModelBase
    {
        private readonly ICatalogDataService _catalogDataService;
        private ObservableCollection<Pie> _piesOfTheWeek;

        public HomeViewModel(IConnectionService connectionService,
            INavigationService navigationService,
            IDialogService dialogService,
            ICatalogDataService catalogDataService) : base(connectionService, navigationService, dialogService)
        {
            _catalogDataService = catalogDataService;

            PiesOfTheWeek = new ObservableCollection<Pie>();
        }

        public ICommand PieTappedCommand => new Command<Pie>(OnPieTapped);
        public ICommand AddToCartCommand => new Command<Pie>(OnAddToCart);

        public ObservableCollection<Pie> PiesOfTheWeek
        {
            get => _piesOfTheWeek;
            set
            {
                _piesOfTheWeek = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
        {
            PiesOfTheWeek = (await _catalogDataService.GetPiesOfTheWeekAsync()).ToObservableCollection();
        }

        private void OnPieTapped(Pie selectedPie)
        {
            _navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
        }

        private async void OnAddToCart(Pie selectedPie)
        {
            MessagingCenter.Send(this, MessagingConstants.AddPieToBasket, selectedPie);
            await _dialogService.ShowDialog("Pie added to your cart", "Success", "OK");
        }
    }
}
