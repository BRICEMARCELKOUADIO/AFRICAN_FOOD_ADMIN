using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Contracts.Services.General
{
    public interface ISettingsService
    {
        void AddItem(string key, string value);
        string GetItem(string key);
        void AddItem2(string key, double value);
        double GetItem2(string key);

        string UserNameSetting { get; set; }
        string UserIdSetting { get; set; }
        string UserPhone { get; set; }
        string Longitude { get; set; }
        string Latitude { get; set; }
        string Position { get; set; }
        string Email { get; set; }
        string UserLastName { get; set; }
        string CommerceName { get; set; }
        string CommerceLocate { get; set; }
    }
}
