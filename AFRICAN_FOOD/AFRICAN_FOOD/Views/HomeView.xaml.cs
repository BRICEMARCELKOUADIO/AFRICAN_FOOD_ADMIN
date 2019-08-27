using AFRICAN_FOOD.Bootstrap;
using AFRICAN_FOOD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AFRICAN_FOOD.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        //public HomeViewModel ViewModel { get; set; }
        public HomeView()
        {
            InitializeComponent();
        }

        //protected async override void OnAppearing()
        //{
        //    ViewModel = AppContainer.Resolve<HomeViewModel>();
        //    await ViewModel.InitializeAsync(null);
        //    this.BindingContext = ViewModel;
        //    base.OnAppearing();

        //}
    }
}