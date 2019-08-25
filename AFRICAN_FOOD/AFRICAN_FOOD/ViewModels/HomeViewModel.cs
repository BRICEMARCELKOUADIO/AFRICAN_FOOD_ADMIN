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
        private ObservableCollection<Pie> _piesOfTheWeek;

        public HomeViewModel(IConnectionService connectionService,
            INavigationService navigationService,
            IDialogService dialogService,
            ICatalogDataService catalogDataService) : base(connectionService, navigationService, dialogService)
        {
            _catalogDataService = catalogDataService;

            PiesOfTheWeek = new ObservableCollection<Pie>();
            //initialise();
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

        private static Dictionary<string, Category> _categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    var categoryList = new[]
                    {
                        new Category { CategoryName = "Fruit pies" },
                        new Category { CategoryName = "Cheese cakes" },
                        new Category { CategoryName = "Seasonal pies" }
                    };

                    _categories = new Dictionary<string, Category>();

                    foreach (Category category in categoryList)
                    {
                        _categories.Add(category.CategoryName, category);
                    }
                }

                return _categories;
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
            await _dialogService.ShowDialog("Produit ajoutée à votre panier", "Succès", "OK");
        }

        public void initialise()
        {
            //PiesOfTheWeek = new ObservableCollection<Pie>()
            //{
            //        new Pie { Name = "Tarte aux pêches", Price = 15.95M, ShortDescription = "Doux comme la pêche", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Fruit pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpie.jpg", InStock = false, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/peachpiesmall.jpg", AllergyInformation = "" },
            //        new Pie { Name = "Tarte à la citrouille", Price = 12.95M, ShortDescription = "Notre préféré Halloween", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Seasonal pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg", InStock = true, IsPieOfTheWeek = true, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg", AllergyInformation = "" },
            //        new Pie { Name = "tarte à la ", Price = 15.95M, ShortDescription = "Mon Dieu, si gentil!", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Fruit pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg", InStock = true, IsPieOfTheWeek = true, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg", AllergyInformation = "" },
            //        new Pie { Name = "Tarte à la fraise", Price = 15.95M, ShortDescription = "Notre délicieuse tarte aux fraises!", LongDescription = "Glaçage gâteau aux carottes Jelly-o Cheesecake. Sweet roll pain de tootsie à la guimauve au caramel à la guimauve et au brownie. Gateau au chocolat pain d 'epice tootsie rouler gateau a l' avoine tarte barre de chocolat drage brownie. Sucette à la barbe à papa en coton. Tarte dragée aux cannes de bonbon. Pâte à sucer dragée gélatine sucette jujubes barre de chocolat cannes de bonbon. Glaçage pain d'épice chupa chups coton bonbon biscuit bonbons glaçage bonbons gélifiés. Gummies brownie sucette biscuit gâteau au chocolat danois. Tarte danoise au beignet au chocolat et aux macarons. Gâteau à la carotte dragée croissant citron gouttes réglisse citron gouttes biscuit sucette au caramel. Gâteau aux carottes Gâteau aux carottes Réglisse Sucre Prune surmontant des jujubes à la tarte aux bonbons. Les caramels à tarte gaufrette gaufrette portent une griffe. Tiramisu tarte tarte au citron danois gouttes. Brownie cupcake dragée gummies.", Category = Categories["Fruit pies"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg", InStock = true, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg", AllergyInformation = "" },
            //        new Pie { Name = "Gâteau Au Fromage Aux Fraises", Price = 18.95M, ShortDescription = "Tu vas l'adorer!", LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", Category = Categories["Cheese cakes"], ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecake.jpg", InStock = false, IsPieOfTheWeek = false, ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecakesmall.jpg", AllergyInformation = "" }

            //};
        }

        public Image byteArrayToImage(byte[] byteArray)
        {
            Image image = new Image();
            Stream stream = new MemoryStream(byteArray);
            image.Source = ImageSource.FromStream(()=> stream);
            return image;
        }
    }
}
