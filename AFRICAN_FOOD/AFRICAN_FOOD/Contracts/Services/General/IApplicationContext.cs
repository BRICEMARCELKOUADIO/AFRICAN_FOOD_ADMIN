using Com.OneSignal.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Contracts.Services.General
{
    public interface IApplicationContext
    {
        event EventHandler HandleNotification;
        string OneSignalUserID { get; set; }
        string OneSignalPushToken { get; set; }
        void HandleNotificationReceived(OSNotification notification);
    }
}
