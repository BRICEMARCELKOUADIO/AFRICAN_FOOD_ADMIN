using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AFRICAN_FOOD.Contracts.Services.General
{
    public interface IConnectionService
    {
        bool IsConnected { get; }
        event ConnectivityChangedEventHandler ConnectivityChanged;
    }
}
