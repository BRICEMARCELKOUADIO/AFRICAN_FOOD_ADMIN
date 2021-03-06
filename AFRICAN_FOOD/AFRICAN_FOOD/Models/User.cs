﻿using Newtonsoft.Json;
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
            public string CommerceName { get; set; }

            [JsonProperty("commerceLocate")]
            public string CommerceLocate { get; set; }

            [JsonProperty("typeUser")]
            public bool TypeUser { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public string PositionGeo { get; set; }


        [JsonProperty("password")]
            public object Password { get; set; }
    }
}
