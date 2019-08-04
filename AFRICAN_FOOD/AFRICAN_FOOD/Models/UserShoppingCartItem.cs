using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Models
{
    public class UserShoppingCartItem
    {
        public string UserId { get; set; }
        public ShoppingCartItem ShoppingCartItem { get; set; }
    }
}
