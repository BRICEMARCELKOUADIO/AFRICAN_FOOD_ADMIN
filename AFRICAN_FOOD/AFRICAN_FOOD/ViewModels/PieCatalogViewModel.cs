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
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AFRICAN_FOOD.ViewModels
{
    public class PieCatalogViewModel : ViewModelBase
    {
        private readonly ICatalogDataService _catalogDataService;
        private readonly ISettingsService _settingsService;

        private bool _photoOk = true;
        private bool _infoUser1 = false;
        private string _productName = string.Empty;
        private string _shortDescription = string.Empty;
        private string _longDescription = string.Empty;
        private string _prixNormal = string.Empty;
        private string _prixPromotionnel = string.Empty;
        private string _quantiteStock = string.Empty;
        private bool _isModify = false;
        private bool _canGo = false;

        private ImageSource _imgSrce;

        private Pie _pie = new Pie();

        public PieCatalogViewModel(IConnectionService connectionService,
            INavigationService navigationService, IDialogService dialogService,
            ICatalogDataService catalogDataService, ISettingsService settingsService)
            : base(connectionService, navigationService, dialogService)
        {
            _catalogDataService = catalogDataService;
            _settingsService = settingsService;
        }

        public ICommand PieTappedCommand => new Command<Pie>(OnPieTapped);
        public ICommand OnTakePicture => new Command(OnTakePictureCommand);
        public ICommand OnNextStep => new Command(OnNextStepCommand);
        public ICommand OnValidate => new Command(OnValiderCommand);

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

        public bool IsModify
        {
            get => _isModify;
            set
            {
                _isModify = value;
                OnPropertyChanged();

            }
        }

        public Pie Pie
        {
            get => _pie;
            set
            {
                _pie = value;
                OnPropertyChanged();
            } 
        }

        public string ShortDescription
        {
            get { return _shortDescription; }
            set {
                _shortDescription = value;
                OnPropertyChanged();
                CanExecute();
            }
        }


        public string LongDescription
        {
            get{ return _longDescription; }
            set
            {
                _longDescription = value;
                OnPropertyChanged();
            }
        }

        public string PrixNormal
        {
            get { return _prixNormal; }
            set
            {
                _prixNormal = value;
                OnPropertyChanged();
                CanExecute();
            }
        }

        public string ProductName
        {
            get { return _productName; }
            set {
                _productName = value;
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

        public string QuantiteStock
        {
            get { return _quantiteStock; }
            set
            {
                _quantiteStock = value;
                OnPropertyChanged();
                CanExecute();
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


        public string PrixPromotionnel
        {
            get { return _prixPromotionnel; }
            set
            {
                _prixPromotionnel = value;
                OnPropertyChanged();
                CanExecute();
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

        private void OnNextStepCommand()
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
            IsBusy = true;
            if (decimal.Parse(_prixNormal) <= decimal.Parse(_prixPromotionnel)) 
            {
                await _dialogService.ShowDialog("Le prix promotionnel doit être inferieur au prix normal!", "", "OK");
                IsBusy = false;
                return;
            }
            //var file = FileSystem.AppDataDirectory
            byte[] byteImage = string.IsNullOrEmpty(srcFile) ? default : await converterImgByte(srcFile);
            string imageToBase64 = string.Empty;
            if (srcFile != null)
            {
                //byteImage = await converterImgByte(srcFile);
                imageToBase64 = System.Convert.ToBase64String(byteImage, Base64FormattingOptions.None);
            }
            else
            {
                imageToBase64 = Pie.Image;

            }
            var noPrix = decimal.Parse(_prixNormal);
            var prPrix = decimal.Parse(_prixPromotionnel);
            var userid = _settingsService.UserIdSetting;
            var userphone = _settingsService.UserPhone;
            var latitude = _settingsService.Latitude;
            var longitude = _settingsService.Longitude;
            var position = _settingsService.Position;
            int piId = Pie == null ? 0 : Pie.PieId;
            int quantite = int.Parse(QuantiteStock);
            var response = await _catalogDataService.AddPiesAsync(piId, _productName, _shortDescription, noPrix, prPrix, imageToBase64, true, true, userid, userphone,longitude,latitude,position,IsModify, quantite);

            if (response != null)
            {
                if(IsModify)
                    await _dialogService.ShowDialog("Votre produit a été modifié avec succès!", "", "OK");
                else
                    await _dialogService.ShowDialog("Votre produit a été ajouté avec succès!", "", "OK");

                MessagingCenter.Send<PieCatalogViewModel>(this, "Pies_added");
                await _navigationService.PopToRootAsync();
            }
            else
            {
                await _dialogService.ShowDialog("Veuillez vérifier les informations entrées", "Erreur", "OK");
                IsBusy = false;
                return;
            }
            IsBusy = false;
            
        }

        private void CanExecute()
        {
            //CanGo = !(string.IsNullOrEmpty(PrixNormal) || string.IsNullOrEmpty(ProductName) || string.IsNullOrEmpty(QuantiteStock) || string.IsNullOrEmpty(ShortDescription) || string.IsNullOrEmpty(PrixPromotionnel));
        }


        private async Task<byte[]> converterImgByte(string img)
        {
            var webClient = new WebClient();
            byte[] imageBytes = await webClient.DownloadDataTaskAsync(new Uri(img));
            return imageBytes;
        }

        private ImageSource converteByteToImage(object value)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                string source = value as string;
                byte[] imageAsBytes = System.Convert.FromBase64String(source);
                retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            }
            return retSource;
        }

        private void OnPieTapped(Pie selectedPie)
        {
            _navigationService.NavigateToAsync<PieDetailViewModel>(selectedPie);
        }

        public override async Task InitializeAsync(object data)
        {

            Pie = (Pie)data;
            if (Pie != null)
            {
                ImgSrce = converteByteToImage(Pie.Image);
                PrixNormal = (Pie.Price).ToString();
                PrixPromotionnel = (Pie.PrixPromotionnel).ToString();
                ProductName = Pie.Name;
                ShortDescription = Pie.ShortDescription;
                PhotoOk = true;
                IsModify = true;
            }

        }
    }
}
