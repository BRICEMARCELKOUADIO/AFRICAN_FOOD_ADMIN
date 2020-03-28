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
        private const string _commerceName = "_commerceName";
        private const string _commerceLocate = "_commerceLocate";
        private const string _userLastName = "_userLastName";
        private const string _email = "_email";


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
        public string Longitude
        {
            get => GetItem(_longitude);
            set => AddItem(_longitude, value);
        }
        public string Latitude
        {
            get => GetItem(_latitude);
            set => AddItem(_latitude, value);
        }
        public string Position
        {
            get => GetItem(_position);
            set => AddItem(_position, value);
        }

        public string CommerceName
        {
            get => GetItem(_commerceName);
            set => AddItem(_commerceName, value);
        }

        public string UserLastName
        {
            get => GetItem(_userLastName);
            set => AddItem(_userLastName, value);
        }

        public string CommerceLocate
        {
            get => GetItem(_commerceLocate);
            set => AddItem(_commerceLocate, value);
        }

        public string Email
        {
            get => GetItem(_email);
            set => AddItem(_email, value);
        }
    }
}
