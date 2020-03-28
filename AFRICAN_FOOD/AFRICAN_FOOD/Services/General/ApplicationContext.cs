using AFRICAN_FOOD.Contracts.Services.General;
using Com.OneSignal.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Services.General
{
    public class ApplicationContext : IApplicationContext
    {
        public string OneSignalUserID { get; set; }
        public string OneSignalPushToken { get; set; }

        public event EventHandler HandleNotification;

        public void HandleNotificationReceived(OSNotification notification)
        {
            HandleNotification?.Invoke(this, null);
        }
    }
}
