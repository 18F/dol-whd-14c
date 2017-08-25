using System.ComponentModel;
using System.Configuration;

namespace DOL.WHD.Section14c.Common
{
    public static class AppSettings
    {
        public static T Get<T>(string key)
        {
            var appSetting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(appSetting)) throw new SettingsPropertyNotFoundException(key);

            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)(converter.ConvertFromInvariantString(appSetting));
        }
    }
}
