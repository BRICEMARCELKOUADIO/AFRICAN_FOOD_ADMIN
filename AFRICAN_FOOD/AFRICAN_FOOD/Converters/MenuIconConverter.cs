using AFRICAN_FOOD.Enumerations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AFRICAN_FOOD.Converters
{
    public class MenuIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (MenuItemType)value;

            switch (type)
            {
                case MenuItemType.Home:
                    return "ic_home.png";
                case MenuItemType.Contact:
                    return "ic_contact.png";
                case MenuItemType.Pies:
                    return "ic_pies.png";
                case MenuItemType.ShoppingCart:
                    return "ic_cart.png";
                case MenuItemType.Logout:
                    return "ic_logout.png";
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Not needed here
            throw new NotImplementedException();
        }
    }
}
