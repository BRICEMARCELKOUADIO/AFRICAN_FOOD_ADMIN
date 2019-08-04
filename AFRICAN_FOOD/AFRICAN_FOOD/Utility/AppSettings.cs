using AFRICAN_FOOD.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using AFRICAN_FOOD.Extensions;

namespace AFRICAN_FOOD.Utility
{
    public static class AppSettings
    {
        private static ISettings Settings => CrossSettings.Current;

        public static User User
        {
            get => Settings.GetValueOrDefault(nameof(User), default(User));

            set => Settings.AddOrUpdateValue(nameof(User), value);
        }
    }
}
