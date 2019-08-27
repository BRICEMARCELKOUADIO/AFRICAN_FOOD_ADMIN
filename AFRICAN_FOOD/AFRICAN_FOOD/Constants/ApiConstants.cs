using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Constants
{
    public class ApiConstants
    {
        public const string BaseApiUrl = "https://africanfoodapi.azurewebsites.net/";
        public const string CatalogEndpoint = "api/catalog/pies/";
        public const string PiesOfTheWeekEndpoint = "api/catalog/piesoftheweek/";
        public const string ShoppingCartEndpoint = "api/shoppingcart";
        public const string AddShoppingCartItemEndpoint = "api/shoppingcart/";
        public const string AddContactInfoEndpoint = "api/contact";
        public const string PlaceOrderEndpoint = "api/order";
        public const string RegisterEndpoint = "api/authentication/register";
        public const string AuthenticateEndpoint = "api/authentication/authenticate";
        public const string AddPies = "api/catalog/AddPies_2";
        public const string GetPieByAdminId = "api/catalog/GetPieByAdminId/";
    }
}
