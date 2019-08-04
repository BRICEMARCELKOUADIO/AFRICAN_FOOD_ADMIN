using AFRICAN_FOOD.Contracts.Services.General;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Services.General
{
    public class PhoneService : IPhoneService
    {
        public void MakePhoneCall()
        {
            CrossMessaging.Current.PhoneDialer.MakePhoneCall("5554885002");
        }
    }
}
