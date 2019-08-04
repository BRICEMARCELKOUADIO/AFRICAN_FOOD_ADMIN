using AFRICAN_FOOD.Contracts.Services.Data;
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
    public class CheckoutViewModel : ViewModelBase
    {
        private readonly IOrderDataService _orderDataService;
        private readonly ISettingsService _settingsService;
        private Order _order;

        public CheckoutViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            IOrderDataService orderDataService, ISettingsService settingsService)
            : base(connectionService, navigationService, dialogService)
        {
            _orderDataService = orderDataService;
            _settingsService = settingsService;
        }

        public ICommand PlaceOrderCommand => new Command(OnPlaceOrder);

        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged();
            }
        }

        public override Task InitializeAsync(object data)
        {
            Order = new Order { OrderId = Guid.NewGuid().ToString(), Address = new Address(), UserId = _settingsService.UserIdSetting };

            return base.InitializeAsync(data);
        }

        private async void OnPlaceOrder()
        {
            await _orderDataService.PlaceOrder(Order);
            MessagingCenter.Send(this, "OrderPlaced");
            await _dialogService.ShowDialog("Order placed successfully", "Success", "OK");
            await _navigationService.PopToRootAsync();
        }
    }
}
