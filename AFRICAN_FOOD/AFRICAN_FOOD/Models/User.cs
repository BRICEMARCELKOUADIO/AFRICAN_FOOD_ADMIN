using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Models
{
    public class User
    {

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("userPhone")]
            public string UserPhone { get; set; }

            [JsonProperty("firstName")]
            public string FirstName { get; set; }

            [JsonProperty("lastName")]
            public string LastName { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("commerceName")]
            public object CommerceName { get; set; }

            [JsonProperty("commerceLocate")]
            public string CommerceLocate { get; set; }

            [JsonProperty("typeUser")]
            public bool TypeUser { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string PositionGeo { get; set; }


        [JsonProperty("password")]
            public object Password { get; set; }
    }
}
