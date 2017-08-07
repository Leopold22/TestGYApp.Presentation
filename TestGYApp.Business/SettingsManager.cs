using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGYApp.Business
{
    public class SettingsManager
    {
        public static List<DTO.GlobalSetting> GetGlobalSettings()
        {
            var globalSettings = Data.SettingsRepository.GetGlobalSettings();
            return globalSettings;
        }


        public static void UpdateGlobalSetting(int id, string systemName, string description, string value)
        {
            Data.SettingsRepository.UpdateGlobalSetting(id, systemName, description, value);

        }

    }
}
