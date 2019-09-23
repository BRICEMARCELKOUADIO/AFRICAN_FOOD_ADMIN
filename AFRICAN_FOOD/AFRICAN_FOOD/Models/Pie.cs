using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Models
{
    public class Pie
    {

        public int PieId { get; set; }
        public string Name { get; set; }

      
        public string ShortDescription { get; set; }

        //[StringLength(2000)]
        //public string LongDescription { get; set; }

        //[StringLength(500)]
        //public string AllergyInformation { get; set; }

      
        public decimal Price { get; set; }
        
        public decimal PrixPromotionnel { get; set; }
        //[Required]
        //public string ImageUrl { get; set; }
        //[Required]
        //public string ImageThumbnailUrl { get; set; }

        
        public string Image { get; set; }

        public bool IsPieOfTheWeek { get; set; }

        public bool InStock { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string PositionGeo { get; set; }

        public string UserAdminId { get; set; }
        public string UserPhone{ get; set; }
    }
}
