using AFRICAN_FOOD.Constants;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.Models;
using AFRICAN_FOOD.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AFRICAN_FOOD.ViewModels
{
    public class PieDetailViewModel : ViewModelBase
    {
        private Pie _selectedPie;

        public PieDetailViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService)
            : base(connectionService, navigationService, dialogService)
        { }

        public ICommand AddToCartCommand => new Command(OnAddToCart);
        public ICommand ReadDescriptionCommand => new Command(OnReadDescription);

        public Pie SelectedPie
        {
            get => _selectedPie;
            set
            {
                _selectedPie = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object pie)
        {
            SelectedPie = (Pie)pie;
        }

        private async void OnAddToCart()
        {
            MessagingCenter.Send(this, MessagingConstants.AddPieToBasket, SelectedPie);
            await _dialogService.ShowDialog("Pie added to your cart", "Success", "OK");
        }

        private void OnReadDescription()
        {
            DependencyService.Get<ITextToSpeech>().ReadText(SelectedPie.LongDescription);
        }
    }
}
