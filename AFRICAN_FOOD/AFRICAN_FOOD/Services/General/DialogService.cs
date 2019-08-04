using Acr.UserDialogs;
using AFRICAN_FOOD.Contracts.Services.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFRICAN_FOOD.Services.General
{
    public class DialogService : IDialogService
    {
        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public void ShowToast(string message)
        {
            UserDialogs.Instance.Toast(message);
        }
    }

}
