using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Contracts.Services.General;
using AFRICAN_FOOD.Extensions;
using AFRICAN_FOOD.Models;
using AFRICAN_FOOD.ViewModels.Base;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AFRICAN_FOOD.ViewModels
{
    public class PieCatalogViewModel : ViewModelBase
    {
        private readonly ICatalogDataService _catalogDataService;

        private bool _photoOk = true;
        private bool _infoUser1 = false;
        private ImageSource _imgSrce;

        private ObservableCollection<Pie> _pies;

        public PieCatalogViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            ICatalogDataService catalogDataService)
            : base(connectionService, navigationService, dialogService)
        {
            _catalogDataService = catalogDataService;
        }

        public ICommand PieTappedCommand => new Command<Pie>(OnPieTapped);
        public ICommand OnTakePicture => new Command(OnTakePictureCommand);
        public ICommand OnNextStep => new Command(OnNextStepCommand);
        public ICommand OnValider => new Command(OnValiderCommand);

        //public ObservableCollection<Pie> Pies
        //{
        //    get => _pies;
        //    set
        //    {
        //        _pies = value;
        //        OnPropertyChanged();
        //    }
        //}

        public ImageSource ImgSrce
        {
            get { return _imgSrce; }
            set
            {
                _imgSrce = value;
                OnPropertyChanged();
            }
        }

        public bool PhotoOk
        {
            get { return _photoOk; }
            set
            {
                _photoOk = value;
                OnPropertyChanged();
            }
        }

        public bool InfoUser1
        {
            get { return _infoUser1; }
            set
            {
                _infoUser1 = value;
                OnPropertyChanged();
            }
        }

        private string _srcFile;
        public string srcFile
        {
            get
            {
                return _srcFile;
            }
            set
            {
                _srcFile = value;
                OnPropertyChanged();
            }

        }

        private async void OnTakePictureCommand()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await _dialogService.ShowDialog("No camera avaialble.", "", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                CustomPhotoSize = 10, //Resize to 10% of original
                CompressionQuality = 12,
                SaveToAlbum = false
            });

            if (file == null) return;

            //await _pageDialog.DisplayAlertAsync("File Location", file.Path, "OK");
            srcFile = file.Path;

            ImgSrce = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();

                return stream;
            });

            //GoInfoUser = true;
        }

        private async void OnNextStepCommand()
        {
            if (PhotoOk)
            {
                InfoUser1 = true;
                PhotoOk = false;
                return;
            }
        }

        public async void OnValiderCommand()
        {
            await _dialogService.ShowDialog("Produit Ajouter", "", "OK");
            await _navigationService.NavigateToAsync<MainViewModel>();
        }

        private void OnPieTapped(Pie selectedPie)
        {
            _navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
        }

        //public override async Task InitializeAsync(object data)
        //{
        //    IsBusy = true;

        //    Pies = (await _catalogDataService.GetAllPiesAsync()).ToObservableCollection();

        //    IsBusy = false;
        //}
    }
}
