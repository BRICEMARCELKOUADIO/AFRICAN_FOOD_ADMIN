using System;
using System.Collections.Generic;
using System.Text;
using AFRICAN_FOOD.Contracts.Services.General;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AFRICAN_FOOD.Services.General
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettings _settings;
        private const string UserName = "UserName";
        private const string UserId = "UserId";
        private const string _userPhone = "_userPhone";
        private const string _position = "_position";
        private const string _longitude = "_longitude";
        private const string _latitude = "_latitude";


        public SettingsService()
        {
            _settings = CrossSettings.Current;
        }

        public void AddItem(string key, string value)
        {
            _settings.AddOrUpdateValue(key, value);
        }

        public void AddItem2(string key, double value)
        {
            _settings.AddOrUpdateValue(key, value);
        }

        public string GetItem(string key)
        {
            return _settings.GetValueOrDefault(key, string.Empty);
        }

        public double GetItem2(string key)
        {
            return _settings.GetValueOrDefault(key,0.0);
        }

        public string UserNameSetting
        {
            get => GetItem(UserName);
            set => AddItem(UserName, value);
        }

        public string UserIdSetting
        {
            get => GetItem(UserId);
            set => AddItem(UserId, value);
        }
        public string UserPhone
        {
            get => GetItem(_userPhone);
            set => AddItem(_userPhone, value);
        }
        public double Longitude
        {
            get => GetItem2(_longitude);
            set => AddItem2(_longitude, value);
        }
        public double Latitude
        {
            get => GetItem2(_latitude);
            set => AddItem2(_latitude, value);
        }
        public string Position
        {
            get => GetItem(_position);
            set => AddItem(_position, value);
        }
    }
}
